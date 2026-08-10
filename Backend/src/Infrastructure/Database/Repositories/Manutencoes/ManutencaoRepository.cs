using Contracts.Repositories.Manutencoes;
using Domain.Manutencoes;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories.Manutencoes;

public class ManutencaoRepository : IManutencaoRepository
{
    private readonly MicarDbContext _dbContext;

    public ManutencaoRepository(MicarDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Manutencao?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.Manutencoes
            .Include(m => m.Veiculo)
            .Include(m => m.RegistroOdometro)
            .FirstOrDefaultAsync(m => m.Id == id, ct);
    }

    public async Task<List<Manutencao>> GetAllByVeiculoIdAsync(Guid veiculoId, CancellationToken ct)
    {
        return await _dbContext.Manutencoes
            .Include(m => m.RegistroOdometro)
            .Where(m => m.VeiculoId == veiculoId)
            .ToListAsync(ct);
    }

    public void Remove(Manutencao manutencao)
    {
        _dbContext.Manutencoes.Remove(manutencao);
    }
}
