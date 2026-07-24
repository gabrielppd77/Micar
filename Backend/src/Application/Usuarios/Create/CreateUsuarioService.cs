using Application.Usuarios.Common;
using Contracts.Authentications;
using Contracts.Repositories;
using Contracts.Repositories.Usuarios;
using Domain.Exceptions;
using Domain.Usuarios;

namespace Application.Usuarios.Create;

public class CreateUsuarioService
{
    public const int PasswordMinLength = 6;

    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUsuarioService(
        IUsuarioRepository usuarioRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthenticationResponse> ExecuteAsync(CreateUsuarioRequest request, CancellationToken ct)
    {
        var usuarioEncontrado = await _usuarioRepository.GetByEmailAsync(request.Email, ct);

        if (usuarioEncontrado is not null)
            throw new BadRequestException("O email informado já está cadastrado.");

        if (request.Password.Length < PasswordMinLength)
            throw new BadRequestException($"A senha deve ter no mínimo {PasswordMinLength} caracteres.");

        var hashedPassword = _passwordHasher.HashPassword(request.Password);

        var usuario = new Usuario(request.Name, request.Email, hashedPassword);

        await _usuarioRepository.AddAsync(usuario, ct);

        await _unitOfWork.SaveChangesAsync(ct);

        var token = _jwtTokenGenerator.GenerateToken(usuario);

        return new AuthenticationResponse() { Token = token };
    }
}
