using Domain.Usuarios;

namespace Contracts.Authentications;

public interface IJwtTokenGenerator
{
    string GenerateToken(Usuario usuario);
}
