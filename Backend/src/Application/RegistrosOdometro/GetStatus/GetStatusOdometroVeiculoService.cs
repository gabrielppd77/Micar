using Application.RegistrosOdometro.GetStatus;
using Contracts.Authentications;
using Contracts.Repositories.Veiculos;
using Domain.Exceptions;

namespace Application.RegistrosOdometro.GetStatus;

public class GetStatusOdometroVeiculoService
{
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly ICurrentUsuarioService _currentUsuarioService;

    public GetStatusOdometroVeiculoService(
        IVeiculoRepository veiculoRepository,
        ICurrentUsuarioService currentUsuarioService)
    {
        _veiculoRepository = veiculoRepository;
        _currentUsuarioService = currentUsuarioService;
    }

    public async Task<OdometroStatusResponse> ExecuteAsync(Guid veiculoId, CancellationToken ct)
    {
        var usuarioId = _currentUsuarioService.GetUsuarioId();

        var veiculo = await _veiculoRepository.GetByIdAsync(veiculoId, ct);

        if (veiculo is null || veiculo.UsuarioId != usuarioId)
            throw new NotFoundException("Veículo não encontrado.");

        var hoje = DateOnly.FromDateTime(DateTime.UtcNow);

        return new OdometroStatusResponse(veiculo.UltimoRegistroOdometro, hoje);
    }
}
