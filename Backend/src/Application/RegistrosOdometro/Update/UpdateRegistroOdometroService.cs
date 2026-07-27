using Contracts.Authentications;
using Contracts.Repositories;
using Contracts.Repositories.RegistrosOdometro;
using Domain.Exceptions;

namespace Application.RegistrosOdometro.Update;

public class UpdateRegistroOdometroService
{
    private readonly IRegistroOdometroRepository _registroOdometroRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUsuarioService _currentUsuarioService;

    public UpdateRegistroOdometroService(
        IRegistroOdometroRepository registroOdometroRepository,
        IUnitOfWork unitOfWork,
        ICurrentUsuarioService currentUsuarioService)
    {
        _registroOdometroRepository = registroOdometroRepository;
        _unitOfWork = unitOfWork;
        _currentUsuarioService = currentUsuarioService;
    }

    public async Task ExecuteAsync(Guid id, UpdateRegistroOdometroRequest request, CancellationToken ct)
    {
        var usuarioId = _currentUsuarioService.GetUsuarioId();

        var registroOdometro = await _registroOdometroRepository.GetByIdAsync(id, ct);

        if (registroOdometro is null || registroOdometro.Veiculo?.UsuarioId != usuarioId)
            throw new NotFoundException("Registro de odômetro não encontrado.");

        registroOdometro.Atualizar(request.Data, request.Odometro);

        await _unitOfWork.SaveChangesAsync(ct);
    }
}
