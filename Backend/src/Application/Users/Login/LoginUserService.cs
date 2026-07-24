using Application.Users.Common;
using Contracts.Authentications;
using Contracts.Repositories.Users;
using Domain.Exceptions;

namespace Application.Users.Login;

public class LoginUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthenticationResponse> ExecuteAsync(LoginUserRequest request, CancellationToken ct)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, ct);

        if (user is null)
            throw new BadRequestException("Email ou senha inválidos.");

        if (!_passwordHasher.VerifyPassword(request.Password, user.Password))
            throw new BadRequestException("Email ou senha inválidos.");

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResponse() { Token = token };
    }
}
