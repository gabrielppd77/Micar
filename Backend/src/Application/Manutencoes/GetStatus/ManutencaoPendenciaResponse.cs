using Domain.Common;
using Domain.Manutencoes;

namespace Application.Manutencoes.GetStatus;

public class ManutencaoPendenciaResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public NivelAlertaEnum Status { get; set; }
    public DateOnly? DataVencimento { get; set; }
    public int? DiasRestantes { get; set; }
    public int? OdometroVencimento { get; set; }
    public int? KmRestantes { get; set; }

    public ManutencaoPendenciaResponse(Manutencao manutencao, DateOnly hoje, int? odometroAtual)
    {
        Id = manutencao.Id;
        Nome = manutencao.Nome;
        Status = manutencao.CalcularStatus(hoje, odometroAtual);
        DataVencimento = manutencao.DataVencimento;
        DiasRestantes = manutencao.CalcularDiasRestantes(hoje);
        OdometroVencimento = manutencao.OdometroVencimento;
        KmRestantes = manutencao.CalcularKmRestantes(odometroAtual);
    }
}
