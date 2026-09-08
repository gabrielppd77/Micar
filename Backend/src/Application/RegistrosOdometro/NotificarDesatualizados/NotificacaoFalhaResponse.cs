namespace Application.RegistrosOdometro.NotificarDesatualizados;

public class NotificacaoFalhaResponse
{
    public Guid UsuarioId { get; set; }
    public string Erro { get; set; }

    public NotificacaoFalhaResponse(Guid usuarioId, string erro)
    {
        UsuarioId = usuarioId;
        Erro = erro;
    }
}
