using Domain.Common;

namespace Application.Manutencoes.GetStatus;

public class VeiculoStatusManutencaoResponse
{
    public NivelAlertaEnum StatusGeral { get; set; }
    public int QuantidadeTotal { get; set; }
    public int QuantidadeEmDia { get; set; }
    public int QuantidadeProximas { get; set; }
    public int QuantidadeVencidas { get; set; }
    public List<ManutencaoPendenciaResponse> Pendencias { get; set; }

    public VeiculoStatusManutencaoResponse(
        List<ManutencaoPendenciaResponse> todas,
        List<ManutencaoPendenciaResponse> pendencias)
    {
        Pendencias = pendencias;
        QuantidadeTotal = todas.Count;
        QuantidadeVencidas = todas.Count(p => p.Status == NivelAlertaEnum.Critico);
        QuantidadeProximas = todas.Count(p => p.Status == NivelAlertaEnum.Atencao);
        QuantidadeEmDia = QuantidadeTotal - QuantidadeVencidas - QuantidadeProximas;

        StatusGeral = QuantidadeVencidas > 0
            ? NivelAlertaEnum.Critico
            : QuantidadeProximas > 0
                ? NivelAlertaEnum.Atencao
                : NivelAlertaEnum.Normal;
    }
}
