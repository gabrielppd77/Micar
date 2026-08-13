using Domain.Common;
using Domain.RegistrosOdometro;

namespace Application.RegistrosOdometro.GetStatus;

public class OdometroStatusResponse
{
    public NivelAlertaEnum Status { get; set; }
    public DateOnly? UltimaAtualizacao { get; set; }
    public int? Odometro { get; set; }
    public int? DiasSemAtualizacao { get; set; }

    public OdometroStatusResponse(RegistroOdometro? ultimoRegistro, DateOnly hoje)
    {
        Status = ultimoRegistro?.CalcularStatus(hoje) ?? NivelAlertaEnum.Critico;
        UltimaAtualizacao = ultimoRegistro?.Data;
        Odometro = ultimoRegistro?.Odometro;
        DiasSemAtualizacao = ultimoRegistro?.CalcularDiasSemAtualizacao(hoje);
    }
}
