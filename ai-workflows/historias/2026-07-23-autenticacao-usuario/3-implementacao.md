# Implementação: Autenticação de Usuário

## Análise
Projeto Micar é Clean Architecture em .NET 10 (`Domain` / `Application` /
`Contracts` / `Infrastructure` / `Api`), sem nenhum código de usuário/auth
ainda. Padrão observado em `Veiculos` (usado como referência de estilo):
- `Domain/<Feature>/<Entidade>.cs`: herda `Entity` (Guid Id gerado no ctor
  protegido), setters privados, ctor privado sem parâmetros (EF), ctor
  público validando e lançando `BadRequestException`.
- `Contracts/Repositories/<Feature>/I<Feature>Repository.cs`: interface
  enxuta, métodos `...Async(..., CancellationToken ct)`.
- `Infrastructure/Database/Repositories/<Feature>/<Feature>Repository.cs`:
  usa `MicarDbContext` direto, `Add` só marca para tracking (quem persiste é
  o `IUnitOfWork.SaveChangesAsync`).
- `Infrastructure/Database/Configurations/<Feature>Configuration.cs`:
  `IEntityTypeConfiguration<T>`.
- `Application/<Feature>/<Caso de uso>/<Caso>Service.cs` + `...Request.cs`:
  classe concreta (sem interface genérica), `ExecuteAsync(request, ct)`,
  injeção via construtor.
- `Api/Controllers/<Feature>Controller.cs`: controller fino, serviço
  injetado como parâmetro da action (`[FromServices]` implícito).
- DI dividida em `AddApplication`/`AddInfrastructure`/`AddApi`, cada uma no
  seu projeto.

O projeto de referência (ControleFinanceiro) foi analisado só como fonte de
ideias — a estrutura concreta (`IServiceHandler<,>` genérico decorado com
FluentValidation via Scrutor) não será trazida, pois o Micar já resolve isso
de forma mais simples com classes concretas.

Não surgiram dúvidas técnicas bloqueantes; os pontos abaixo são decisões de
projeto que tomei e que ficam abertas para ajuste no "Esse plano está
correto?" (não são bloqueantes para começar).

## Diferenças em relação ao ControleFinanceiro (melhorias sugeridas)
1. **Hash de senha**: ControleFinanceiro usa PBKDF2 manual
   (`Rfc2898DeriveBytes`, sem pacote). Vamos usar **BCrypt** (via
   `BCrypt.Net-Next`), como já combinado — é um algoritmo desenhado
   especificamente para senha (custo ajustável, resistente a hardware
   dedicado) e evita reimplementar detalhes de salt/iterações na mão.
2. **Claims do token**: ControleFinanceiro só inclui `sub`, `name`,
   `given_name`, `jti` — sem e-mail. Vou incluir também a claim de e-mail,
   útil para consumidores do token sem precisar consultar o banco.
3. **Segredo do JWT**: ControleFinanceiro tem o segredo do JWT **hardcoded
   em `appsettings.json`, versionado no git** — não é uma boa prática. O
   `Api.csproj` do Micar já tem `UserSecretsId` configurado; vou colocar só
   um placeholder óbvio em `appsettings.json` (`Secret: ""`) e configurar o
   segredo real via `dotnet user-secrets` neste ambiente de desenvolvimento,
   documentando isso para que em produção venha de variável de ambiente.
4. **Autorização global**: seguindo o que você confirmou, vou aplicar
   `RequireAuthorization()` global em `MapControllers()` (mesmo padrão do
   ControleFinanceiro) e marcar explicitamente `UserController` (ambas
   actions) e `VeiculosController` com `[AllowAnonymous]`.

## Plano de alterações

**Domain**
- `Domain/Users/User.cs` — entidade com `Name`, `Email`, `Password` (hash),
  setters privados, ctor privado (EF) + ctor público validando: Name e
  Email obrigatórios, Email com formato básico válido, Password (já
  recebida com hash) obrigatória. Validação de tamanho mínimo da senha em
  texto puro (**6 caracteres**) acontece no `CreateUserService`, antes do
  hash — a entidade só enxerga o hash, então não é o lugar certo para essa
  regra.

**Contracts**
- `Contracts/Repositories/Users/IUserRepository.cs` — `AddAsync(User, ct)`,
  `GetByEmailAsync(string email, ct)`.
- `Contracts/Authentications/IPasswordHasher.cs` — `HashPassword`,
  `VerifyPassword`.
- `Contracts/Authentications/IJwtTokenGenerator.cs` — `GenerateToken(User)`.

**Application**
- `Application/Users/AuthenticationResponse.cs` — `record
  AuthenticationResponse(string Token)`, compartilhado entre Create e
  Login.
- `Application/Users/Create/CreateUserRequest.cs` (`Name`, `Email`,
  `Password`) + `CreateUserService.cs` — valida duplicidade de e-mail,
  valida tamanho mínimo de senha, faz hash, persiste, gera token.
- `Application/Users/Login/LoginUserRequest.cs` (`Email`, `Password`) +
  `LoginUserService.cs` — busca por e-mail, verifica senha, gera token;
  mensagem de erro genérica ("E-mail ou senha inválidos") tanto para
  usuário inexistente quanto senha errada, para não vazar quais e-mails
  existem.
- `Application/DependencyInjection.cs` — registrar os dois novos serviços.

**Infrastructure**
- `Infrastructure/Authentications/JwtSettings.cs` — POCO (`Secret`,
  `ExpiryMinutes`, `Issuer`, `Audience`).
- `Infrastructure/Authentications/PasswordHasher.cs` — implementação com
  BCrypt.
- `Infrastructure/Authentications/JwtTokenGenerator.cs` — gera token
  HMAC-SHA256 com claims `sub`, `name`, `email`, `jti`, expiração via
  `JwtSettings.ExpiryMinutes`.
- `Infrastructure/Authentications/DependencyInjection.cs` — `AddAuth`:
  registra `JwtSettings` (bind de configuração), `IPasswordHasher`,
  `IJwtTokenGenerator`, `AddAuthentication().AddJwtBearer(...)` com
  `TokenValidationParameters` validando issuer/audience/tempo de vida/chave.
- `Infrastructure/Database/Configurations/UserConfiguration.cs` —
  `HasKey(Id)`, índice único em `Email`.
- `Infrastructure/Database/Repositories/Users/UserRepository.cs`.
- `Infrastructure/Database/Context/MicarDbContext.cs` — adicionar `DbSet<User> Users`.
- `Infrastructure/DependencyInjection.cs` — chamar `AddAuth(configuration)`,
  registrar `IUserRepository`.
- Nova migration EF Core (`AddUser` ou similar) para criar a tabela.
- Pacotes novos: `Microsoft.AspNetCore.Authentication.JwtBearer` (em
  `Infrastructure.csproj`) e `BCrypt.Net-Next` (em `Infrastructure.csproj`).

**Api**
- `Api/Controllers/UserController.cs` — `[AllowAnonymous][HttpPost]` (raiz,
  criação) e `[AllowAnonymous][HttpPost("login")]` (login), delegando para
  `CreateUserService`/`LoginUserService` injetados na action.
- `Api/Controllers/VeiculosController.cs` — adicionar `[AllowAnonymous]`.
- `Api/DependencyInjection.cs` (`UseApi`) — adicionar `app.UseAuthentication()`
  antes de `app.UseAuthorization()`, e `app.MapControllers().RequireAuthorization()`.
- `Backend/src/Api/appsettings.json` — seção `JwtSettings` (Secret vazio/
  placeholder, ExpiryMinutes, Issuer, Audience); configurar segredo real via
  `dotnet user-secrets` neste ambiente de dev.

Esse plano está correto? Posso aplicar as alterações?

## Resultado
Alterações aplicadas conforme o plano acima. Build da solução (`dotnet build
MICAR.slnx`) sem erros. Migration `CreateTableUser` gerada via `dotnet ef
migrations add` (cria tabela `users` com índice único em `email`). Segredo
do JWT configurado via `dotnet user-secrets` no projeto `Api` (não
versionado).

Testado manualmente rodando a API localmente contra o Postgres do
`docker-compose` já em execução:
- `POST /user` (cadastro) → 200 com token JWT.
- `POST /user/login` (login) com credenciais corretas → 200 com token.
- `POST /user/login` com senha errada → 400, mensagem genérica.
- `POST /user` com e-mail duplicado → 400.
- `POST /user` com senha menor que 6 caracteres → 400.
- `POST /veiculos` sem token → 204 (continua público, `[AllowAnonymous]`).

Usuário de teste removido do banco após a validação.

## Ajuste pós-implementação: 401 em POST /veiculos
Usuário reportou 401 ao chamar `POST /veiculos` mesmo enviando um token.
Investigação:
- O container Docker (sessão de debug do Visual Studio) usado pelo usuário
  para testar estava parado/idle (`DistrolessHelper.dll --wait`, sem o
  processo da API rodando) no momento do teste — não é um bug de código.
- Nesse meio-tempo, o código em disco já tinha divergido do plano original
  por edições manuais durante os testes: `VeiculosController` havia perdido
  o `[AllowAnonymous]` (passando a exigir token) e `appsettings.json` tinha
  o segredo do JWT em texto puro (`Secret` preenchido) + `ExpiryMinutes:
  1440`, revertendo o placeholder + `dotnet user-secrets` combinado
  originalmente.
- Rodei a API localmente (fora do container) do zero para validar a lógica
  de JWT isoladamente: cadastro → token; `POST /veiculos` sem token → 401;
  `POST /veiculos` com o token retornado no cadastro → 204. Confirma que
  geração/validação do token está correta.

Decisão do usuário (perguntado via clarificação):
- `VeiculosController` **passa a exigir token** — não restaurar
  `[AllowAnonymous]` (mudança de escopo em relação ao combinado
  inicialmente).
- Segredo do JWT **permanece hardcoded** em `appsettings.json` (com
  `ExpiryMinutes: 1440`) — não usar `dotnet user-secrets` para esse valor.

Ação tomada: removido o user-secret `JwtSettings:Secret` que eu havia
configurado antes (senão ele sobreporia silenciosamente o valor do
`appsettings.json` em Development, já que User Secrets tem precedência).
Nenhuma alteração de código foi necessária além disso — o estado atual dos
arquivos já reflete as decisões acima.
