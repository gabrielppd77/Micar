using Contracts.Repositories.RegistrosOdometro;
using Domain.RegistrosOdometro;
using Infrastructure.Database.Context;

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
}
