using Contracts.Authentications;
using Contracts.Repositories;
using Contracts.Repositories.RegistrosOdometro;
using Domain.Exceptions;

namespace Application.RegistrosOdometro.Delete;

public class DeleteRegistroOdometroService
{
    private readonly IRegistroOdometroRepository _registroOdometroRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUsuarioService _currentUsuarioService;

    public DeleteRegistroOdometroService(
        IRegistroOdometroRepository registroOdometroRepository,
        IUnitOfWork unitOfWork,
        ICurrentUsuarioService currentUsuarioService)
    {
        _registroOdometroRepository = registroOdometroRepository;
        _unitOfWork = unitOfWork;
        _currentUsuarioService = currentUsuarioService;
    }

    public async Task ExecuteAsync(Guid id, CancellationToken ct)
    {
        var usuarioId = _currentUsuarioService.GetUsuarioId();

        var registroOdometro = await _registroOdometroRepository.GetByIdAsync(id, ct);

        if (registroOdometro is null || registroOdometro.Veiculo?.UsuarioId != usuarioId)
            throw new NotFoundException("Registro de odômetro não encontrado.");

        _registroOdometroRepository.Remove(registroOdometro);

        await _unitOfWork.SaveChangesAsync(ct);
    }
}
