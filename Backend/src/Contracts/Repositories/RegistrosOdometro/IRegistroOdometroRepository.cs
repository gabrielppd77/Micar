using Domain.RegistrosOdometro;

namespace Contracts.Repositories.RegistrosOdometro;

public interface IRegistroOdometroRepository
{
    Task AddAsync(RegistroOdometro registroOdometro, CancellationToken ct);
    Task<RegistroOdometro?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<List<RegistroOdometro>> GetAllByVeiculoIdAsync(Guid veiculoId, CancellationToken ct);
    void Remove(RegistroOdometro registroOdometro);
}
