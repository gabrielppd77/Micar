using Domain.Manutencoes;

namespace Contracts.Repositories.Manutencoes;

public interface IManutencaoRepository
{
    Task AddAsync(Manutencao manutencao, CancellationToken ct);
    Task<Manutencao?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<List<Manutencao>> GetAllByVeiculoIdAsync(Guid veiculoId, CancellationToken ct);
    void Remove(Manutencao manutencao);
}
