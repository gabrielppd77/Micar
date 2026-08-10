namespace Application.RegistrosOdometro.Create;

public class CreateRegistroOdometroRequest
{
    public required Guid VeiculoId { get; set; }
    public required int Odometro { get; set; }
}
