using Application.Usuarios.Common;
using Contracts.Authentications;
using Contracts.Repositories.Usuarios;
using Domain.Exceptions;

namespace Application.Usuarios.Login;

public class LoginUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUsuarioService(
        IUsuarioRepository usuarioRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthenticationResponse> ExecuteAsync(LoginUsuarioRequest request, CancellationToken ct)
    {
        var usuario = await _usuarioRepository.GetByEmailAsync(request.Email, ct);

        if (usuario is null)
            throw new BadRequestException("Email ou senha inválidos.");

        if (!_passwordHasher.VerifyPassword(request.Password, usuario.Password))
            throw new BadRequestException("Email ou senha inválidos.");

        var token = _jwtTokenGenerator.GenerateToken(usuario);

        return new AuthenticationResponse() { Token = token };
    }
}
