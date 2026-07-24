using Domain.Usuarios;

namespace Contracts.Repositories.Usuarios;

public interface IUsuarioRepository
{
    Task AddAsync(Usuario usuario, CancellationToken ct);
    Task<Usuario?> GetByEmailAsync(string email, CancellationToken ct);
}
