using Contracts.Repositories.Usuarios;
using Domain.Usuarios;
using Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repositories.Usuarios;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly MicarDbContext _dbContext;

    public UsuarioRepository(MicarDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Usuario usuario, CancellationToken ct)
    {
        await _dbContext.Usuarios.AddAsync(usuario, ct);
    }

    public async Task<Usuario?> GetByEmailAsync(string email, CancellationToken ct)
    {
        return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == email, ct);
    }
}
