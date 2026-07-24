# Entendimento: Autenticação de Usuário

> Status: confirmado
> Confirmado pelo usuário em 2026-07-23

## Objetivo

Criar a entidade `User` (Name, Email, Password) e a infraestrutura de
autenticação do Micar: um serviço de cadastro (Create User) e um serviço de
login (Login User), ambos retornando um token JWT, seguindo os padrões já
adotados no projeto Micar (não os do projeto ControleFinanceiro, que serve
apenas de referência/ponto de partida).

## Contexto / motivação

O Micar ainda não tem nenhuma entidade de usuário nem mecanismo de
autenticação — hoje todos os endpoints (ex: `VeiculosController`) são
públicos. Esta história introduz a base de autenticação que futuras
histórias poderão usar para proteger rotas.

## Escopo

- Entidade `Domain.Users.User` com `Name`, `Email`, `Password` (hash).
- Serviço `CreateUserService` (cadastro de novo usuário).
- Serviço `LoginUserService` (autenticação de usuário existente).
- Geração e configuração de token JWT (chave secreta, emissor, audiência,
  expiração) via `appsettings.json`.
- Hash de senha (nunca armazenar em texto puro).
- Repositório de usuário (`IUserRepository` em `Contracts`, implementação em
  `Infrastructure`), seguindo o mesmo padrão de `IVeiculoRepository`.
- Um único `UserController`, com dois endpoints:
  - `POST /user` — cadastro (Create User).
  - `POST /user/login` — autenticação (Login User).
- Middleware de autenticação JWT aplicado globalmente na API (autenticação
  passa a ser exigida por padrão nas rotas), configurado como no projeto
  ControleFinanceiro (referência de infraestrutura).
- `VeiculosController` passa a exigir autenticação (token JWT válido), como
  qualquer outra rota não marcada com `[AllowAnonymous]` — decisão revista
  durante os testes da etapa de implementação (inicialmente havia sido
  combinado mantê-lo público; o usuário decidiu depois exigir token também
  aqui).

## Fora do escopo

- Refresh token / revogação de token.
- Recuperação de senha, confirmação de e-mail, roles/permissões.
- Qualquer entidade adicional criada automaticamente ao registrar o usuário
  (diferente do ControleFinanceiro, que cria `FinancialTypes` padrão junto
  com o usuário — aqui não há equivalente).

## Critérios de aceite

- É possível criar um usuário informando Name, Email e Password.
- Não é possível criar dois usuários com o mesmo Email.
- A senha é armazenada com hash, nunca em texto puro.
- É possível autenticar (login) com Email e Password corretos e receber um
  token JWT válido.
- Login com credenciais inválidas retorna erro (sem token).
- Configuração de JWT (segredo, emissor, audiência, tempo de expiração) fica
  em `appsettings.json`, seguindo o padrão de configuração já usado no
  projeto (ex: `ConnectionStrings`).

## Suposições e perguntas

Responda diretamente abaixo de cada pergunta, na linha que começa com
`> Resposta:` (pode apagar o placeholder e escrever ali mesmo).

1. O token JWT retornado no login/cadastro deve ser só um access token com
   expiração (sem refresh token), certo?

   > Resposta: certo

2. Deve haver alguma regra mínima de validação de senha (ex: tamanho
   mínimo), ou por enquanto sem regra específica (só não pode ser vazia),
   seguindo o mesmo estilo simples de validação usado em `Veiculo`
   (exceções de domínio, sem FluentValidation)?

   > Resposta: pode ter tamanho minimo e vc pode definir

3. Os endpoints ficam num novo `AuthController` (ex: `POST /auth/register`
   e `POST /auth/login`), no mesmo estilo do `VeiculosController`
   (controller fino delegando pro Service)?

   > Resposta: Na vdd vamos criar um controller para o user, acho q faz mais sentido por enquanto. E colocar os serviços relacionados nas duas chamadas, Post vai ser a criação e o Post + Login, vai ser o auth

4. Nenhum endpoint existente (ex: `VeiculosController`) deve ganhar
   `[Authorize]` nesta história — isso fica para uma história futura,
   correto?

   > Resposta: Inclusive seria bom vc ja aplicar as alterações relacionadas ao Middllare de autenticação, e sim o VeiculosController teria rota publica.

5. Sobre a implementação de JWT: pretendo usar
   `Microsoft.AspNetCore.Authentication.JwtBearer` (pacote oficial da
   Microsoft) para geração/validação do token, com segredo/issuer/audience/
   expiração configuráveis via `appsettings.json` — é essa a abordagem que
   você quer, ou já tinha algo específico em mente?

   > Resposta: Sim isso msmo, se quiser pode usar o projeto que te passei como base, lá tem as configurações na infrastructure tudo certinho

   > Atualização (etapa de implementação): eu havia sugerido manter o
   > segredo fora do `appsettings.json` versionado (placeholder vazio +
   > `dotnet user-secrets`, só em dev). O usuário decidiu manter o segredo
   > real e `ExpiryMinutes: 1440` direto no `appsettings.json` (igual ao
   > ControleFinanceiro), então é assim que ficou — sem user-secrets para
   > esse valor.

6. Para o hash de senha, pretendo usar BCrypt (via pacote `BCrypt.Net-Next`)
   por ser um padrão consolidado e mais seguro que hash manual — ok?
   > Resposta: Concordo cm vc, pode usar o BCrypt

## Observações
