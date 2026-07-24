using Domain.Users;

namespace Contracts.Authentications;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
