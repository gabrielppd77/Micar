using Contracts.Repositories.Veiculos;
using Domain.Veiculos;
using Infrastructure.Database.Context;

namespace Infrastructure.Database.Repositories.Veiculos;

public class VeiculoRepository : IVeiculoRepository
{
    private readonly MicarDbContext _dbContext;

    public VeiculoRepository(MicarDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Veiculo veiculo, CancellationToken ct)
    {
        await _dbContext.Veiculos.AddAsync(veiculo, ct);
    }
}
