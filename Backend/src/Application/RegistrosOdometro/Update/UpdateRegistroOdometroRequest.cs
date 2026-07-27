namespace Application.RegistrosOdometro.Update;

public class UpdateRegistroOdometroRequest
{
    public required DateOnly Data { get; set; }
    public required int Odometro { get; set; }
}
