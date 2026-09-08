using Domain.PushTokens;

namespace Application.PushTokens.Registrar;

public class RegistrarPushTokenRequest
{
    public string Token { get; set; } = string.Empty;
    public PlataformaPushEnum Plataforma { get; set; }
}
