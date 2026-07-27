using Domain.RegistrosOdometro;

namespace Application.RegistrosOdometro.Common;

public class RegistroOdometroResponse
{
    public Guid Id { get; set; }
    public DateOnly Data { get; set; }
    public int Odometro { get; set; }

    public RegistroOdometroResponse(RegistroOdometro registroOdometro)
    {
        Id = registroOdometro.Id;
        Data = registroOdometro.Data;
        Odometro = registroOdometro.Odometro;
    }
}
