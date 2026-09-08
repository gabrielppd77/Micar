namespace Domain.PushNotifications;

public class PushNotificationResultado
{
    public string Token { get; set; } = string.Empty;
    public bool Sucesso { get; set; }
    public bool TokenInvalido { get; set; }
    public string? Mensagem { get; set; }
}
