using Domain.Manutencoes;

namespace Application.Manutencoes.Common;

public class ManutencaoResponse
{
    public Guid Id { get; set; }
    public DateOnly Data { get; set; }
    public string Nome { get; set; }
    public Guid VeiculoId { get; set; }
    public Guid? RegistroOdometroId { get; set; }
    public int? Odometro { get; set; }
    public int? OdometroVencimento { get; set; }
    public DateOnly? DataVencimento { get; set; }

    public ManutencaoResponse(Manutencao manutencao)
    {
        Id = manutencao.Id;
        Data = manutencao.Data;
        Nome = manutencao.Nome;
        VeiculoId = manutencao.VeiculoId;
        RegistroOdometroId = manutencao.RegistroOdometro?.Id;
        Odometro = manutencao.RegistroOdometro?.Odometro;
        OdometroVencimento = manutencao.OdometroVencimento;
        DataVencimento = manutencao.DataVencimento;
    }
}
