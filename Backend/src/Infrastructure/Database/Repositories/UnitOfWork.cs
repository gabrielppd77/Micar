using Contracts.Repositories;
using Infrastructure.Database.Context;

namespace Infrastructure.Database.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MicarDbContext _dbContext;

    public UnitOfWork(MicarDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChangesAsync(CancellationToken ct)
    {
        return _dbContext.SaveChangesAsync(ct);
    }
}
