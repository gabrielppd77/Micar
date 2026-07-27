using Contracts.Repositories.RegistrosOdometro;
using Domain.RegistrosOdometro;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories.RegistrosOdometro;

public class RegistroOdometroRepository : IRegistroOdometroRepository
{
    private readonly MicarDbContext _dbContext;

    public RegistroOdometroRepository(MicarDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(RegistroOdometro registroOdometro, CancellationToken ct)
    {
        await _dbContext.RegistrosOdometro.AddAsync(registroOdometro, ct);
    }

    public async Task<RegistroOdometro?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.RegistrosOdometro
            .Include(r => r.Veiculo)
            .FirstOrDefaultAsync(r => r.Id == id, ct);
    }

    public async Task<List<RegistroOdometro>> GetAllByVeiculoIdAsync(Guid veiculoId, CancellationToken ct)
    {
        return await _dbContext.RegistrosOdometro
            .Where(r => r.VeiculoId == veiculoId)
            .ToListAsync(ct);
    }

    public void Remove(RegistroOdometro registroOdometro)
    {
        _dbContext.RegistrosOdometro.Remove(registroOdometro);
    }
}
