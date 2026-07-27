using Application.RegistrosOdometro.Common;
using Contracts.Authentications;
using Contracts.Repositories.RegistrosOdometro;
using Contracts.Repositories.Veiculos;
using Domain.Exceptions;

namespace Application.RegistrosOdometro.GetAll;

public class GetAllRegistrosOdometroService
{
    private readonly IRegistroOdometroRepository _registroOdometroRepository;
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly ICurrentUsuarioService _currentUsuarioService;

    public GetAllRegistrosOdometroService(
        IRegistroOdometroRepository registroOdometroRepository,
        IVeiculoRepository veiculoRepository,
        ICurrentUsuarioService currentUsuarioService)
    {
        _registroOdometroRepository = registroOdometroRepository;
        _veiculoRepository = veiculoRepository;
        _currentUsuarioService = currentUsuarioService;
    }

    public async Task<List<RegistroOdometroResponse>> ExecuteAsync(Guid veiculoId, CancellationToken ct)
    {
        var usuarioId = _currentUsuarioService.GetUsuarioId();

        var veiculo = await _veiculoRepository.GetByIdAsync(veiculoId, ct);

        if (veiculo is null || veiculo.UsuarioId != usuarioId)
            throw new NotFoundException("Veículo não encontrado.");

        var registrosOdometro = await _registroOdometroRepository.GetAllByVeiculoIdAsync(veiculoId, ct);

        return registrosOdometro.Select(x => new RegistroOdometroResponse(x)).ToList();
    }
}
