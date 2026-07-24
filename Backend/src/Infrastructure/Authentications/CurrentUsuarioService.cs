using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Contracts.Authentications;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Authentications;

public class CurrentUsuarioService : ICurrentUsuarioService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUsuarioService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUsuarioId()
    {
        var usuario = _httpContextAccessor.HttpContext?.User;

        var usuarioIdClaim = usuario?.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? usuario?.FindFirstValue(JwtRegisteredClaimNames.Sub);

        if (string.IsNullOrWhiteSpace(usuarioIdClaim) || !Guid.TryParse(usuarioIdClaim, out var usuarioId))
            throw new UnauthorizedException("Usuário não autenticado.");

        return usuarioId;
    }
}
