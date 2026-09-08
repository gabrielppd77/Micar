using Domain.Common;
using Domain.Exceptions;
using Domain.Usuarios;

namespace Domain.PushTokens;

public class PushToken : Entity
{
    public Guid UsuarioId { get; private set; }
    public Usuario? Usuario { get; private set; }
    public string Token { get; private set; } = string.Empty;
    public PlataformaPushEnum Plataforma { get; private set; }
    public bool Ativo { get; private set; }

    private PushToken()
    {
    }

    public PushToken(Guid usuarioId, string token, PlataformaPushEnum plataforma)
    {
        if (usuarioId == Guid.Empty)
            throw new BadRequestException("Usuário é obrigatório.");

        if (string.IsNullOrWhiteSpace(token))
            throw new BadRequestException("Token é obrigatório.");

        UsuarioId = usuarioId;
        Token = token;
        Plataforma = plataforma;
        Ativo = true;
    }

    public void Desativar()
    {
        Ativo = false;
    }
}
