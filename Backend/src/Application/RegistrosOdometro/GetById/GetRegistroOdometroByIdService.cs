using Application.RegistrosOdometro.Common;
using Contracts.Authentications;
using Contracts.Repositories.RegistrosOdometro;
using Domain.Exceptions;

namespace Application.RegistrosOdometro.GetById;

public class GetRegistroOdometroByIdService
{
    private readonly IRegistroOdometroRepository _registroOdometroRepository;
    private readonly ICurrentUsuarioService _currentUsuarioService;

    public GetRegistroOdometroByIdService(
        IRegistroOdometroRepository registroOdometroRepository,
        ICurrentUsuarioService currentUsuarioService)
    {
        _registroOdometroRepository = registroOdometroRepository;
        _currentUsuarioService = currentUsuarioService;
    }

    public async Task<RegistroOdometroResponse> ExecuteAsync(Guid id, CancellationToken ct)
    {
        var usuarioId = _currentUsuarioService.GetUsuarioId();

        var registroOdometro = await _registroOdometroRepository.GetByIdAsync(id, ct);

        if (registroOdometro is null || registroOdometro.Veiculo?.UsuarioId != usuarioId)
            throw new NotFoundException("Registro de odômetro não encontrado.");

        return new RegistroOdometroResponse(registroOdometro);
    }
}
