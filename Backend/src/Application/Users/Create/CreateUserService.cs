using Application.Users.Common;
using Contracts.Authentications;
using Contracts.Repositories;
using Contracts.Repositories.Users;
using Domain.Exceptions;
using Domain.Users;

namespace Application.Users.Create;

public class CreateUserService
{
    public const int PasswordMinLength = 6;

    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthenticationResponse> ExecuteAsync(CreateUserRequest request, CancellationToken ct)
    {
        var userFound = await _userRepository.GetByEmailAsync(request.Email, ct);

        if (userFound is not null)
            throw new BadRequestException("O email informado já está cadastrado.");

        if (request.Password.Length < PasswordMinLength)
            throw new BadRequestException($"A senha deve ter no mínimo {PasswordMinLength} caracteres.");

        var hashedPassword = _passwordHasher.HashPassword(request.Password);

        var user = new User(request.Name, request.Email, hashedPassword);

        await _userRepository.AddAsync(user, ct);

        await _unitOfWork.SaveChangesAsync(ct);

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResponse() { Token = token };
    }
}
