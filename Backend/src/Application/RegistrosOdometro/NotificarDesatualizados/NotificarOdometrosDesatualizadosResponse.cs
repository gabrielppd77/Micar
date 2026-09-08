namespace Application.RegistrosOdometro.NotificarDesatualizados;

public class NotificarOdometrosDesatualizadosResponse
{
    public int TotalVerificados { get; set; }
    public int TotalNotificados { get; set; }
    public IReadOnlyList<NotificacaoFalhaResponse> Falhas { get; set; }

    public NotificarOdometrosDesatualizadosResponse(int totalVerificados, int totalNotificados, IReadOnlyList<NotificacaoFalhaResponse> falhas)
    {
        TotalVerificados = totalVerificados;
        TotalNotificados = totalNotificados;
        Falhas = falhas;
    }
}
