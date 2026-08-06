using Domain.RegistrosOdometro;

namespace Contracts.Repositories.RegistrosOdometro;

public interface IRegistroOdometroRepository
{
    Task AddAsync(RegistroOdometro registroOdometro, CancellationToken ct);
}
