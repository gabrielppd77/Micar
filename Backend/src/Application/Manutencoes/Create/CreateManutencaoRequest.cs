namespace Application.Manutencoes.Create;

public class CreateManutencaoRequest
{
    public required string Nome { get; set; }
    public required DateOnly Data { get; set; }
    public required Guid VeiculoId { get; set; }
    public required int Odometro { get; set; }
    public int? OdometroVencimento { get; set; }
    public DateOnly? DataVencimento { get; set; }
    public decimal? Valor { get; set; }
}
