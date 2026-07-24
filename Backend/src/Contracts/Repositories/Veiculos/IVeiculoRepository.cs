using Domain.Veiculos;

namespace Contracts.Repositories.Veiculos;

public interface IVeiculoRepository
{
    Task AddAsync(Veiculo veiculo, CancellationToken ct);
    Task<Veiculo?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<List<Veiculo>> GetAllByUsuarioIdAsync(Guid usuarioId, CancellationToken ct);
    void Remove(Veiculo veiculo);
}
