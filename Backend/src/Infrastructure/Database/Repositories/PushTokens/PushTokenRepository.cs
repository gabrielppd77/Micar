using Contracts.Repositories.PushTokens;
using Domain.PushTokens;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories.PushTokens;

public class PushTokenRepository : IPushTokenRepository
{
    private readonly MicarDbContext _dbContext;

    public PushTokenRepository(MicarDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PushToken?> GetByTokenAsync(string token, CancellationToken ct)
    {
        return await _dbContext.PushTokens.FirstOrDefaultAsync(p => p.Token == token, ct);
    }

    public async Task<List<PushToken>> GetAtivosByUsuarioIdAsync(Guid usuarioId, CancellationToken ct)
    {
        return await _dbContext.PushTokens
            .Where(p => p.UsuarioId == usuarioId && p.Ativo)
            .ToListAsync(ct);
    }

    public async Task AddAsync(PushToken pushToken, CancellationToken ct)
    {
        await _dbContext.PushTokens.AddAsync(pushToken, ct);
    }

    public void Remove(PushToken pushToken)
    {
        _dbContext.PushTokens.Remove(pushToken);
    }
}
