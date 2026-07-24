using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Contracts.Authentications;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Authentications;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        var userIdClaim = user?.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? user?.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (string.IsNullOrWhiteSpace(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            throw new UnauthorizedException("Usuário não autenticado.");

        return userId;
    }
}
