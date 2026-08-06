using Contracts.Repositories.Veiculos;
using Domain.Veiculos;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Veiculo?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.Veiculos
            .Include(v => v.RegistrosOdometro.OrderByDescending(r => r.Data).Take(1))
            .FirstOrDefaultAsync(v => v.Id == id, ct);
    }

    public async Task<List<Veiculo>> GetAllByUsuarioIdAsync(Guid usuarioId, CancellationToken ct)
    {
        return await _dbContext.Veiculos
            .Include(v => v.RegistrosOdometro.OrderByDescending(r => r.Data).Take(1))
            .Where(v => v.UsuarioId == usuarioId)
            .ToListAsync(ct);
    }

    public void Remove(Veiculo veiculo)
    {
        _dbContext.Veiculos.Remove(veiculo);
    }
}
