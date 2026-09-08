using Domain.PushTokens;

namespace Contracts.Repositories.PushTokens;

public interface IPushTokenRepository
{
    Task<PushToken?> GetByTokenAsync(string token, CancellationToken ct);
    Task<List<PushToken>> GetAtivosByUsuarioIdAsync(Guid usuarioId, CancellationToken ct);
    Task AddAsync(PushToken pushToken, CancellationToken ct);
    void Remove(PushToken pushToken);
}
