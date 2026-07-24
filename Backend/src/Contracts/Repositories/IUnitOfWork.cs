namespace Contracts.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken ct);
}
