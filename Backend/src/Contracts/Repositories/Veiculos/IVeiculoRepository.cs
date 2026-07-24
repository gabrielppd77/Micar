using Domain.Veiculos;

namespace Contracts.Repositories.Veiculos;

public interface IVeiculoRepository
{
    Task AddAsync(Veiculo veiculo, CancellationToken ct);
}
