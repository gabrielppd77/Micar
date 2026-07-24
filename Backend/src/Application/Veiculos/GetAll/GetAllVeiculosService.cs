using Application.Veiculos.Common;
using Contracts.Authentications;
using Contracts.Repositories.Veiculos;

namespace Application.Veiculos.GetAll;

public class GetAllVeiculosService
{
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly ICurrentUsuarioService _currentUsuarioService;

    public GetAllVeiculosService(
        IVeiculoRepository veiculoRepository,
        ICurrentUsuarioService currentUsuarioService)
    {
        _veiculoRepository = veiculoRepository;
        _currentUsuarioService = currentUsuarioService;
    }

    public async Task<List<VeiculoResponse>> ExecuteAsync(CancellationToken ct)
    {
        var usuarioId = _currentUsuarioService.GetUsuarioId();

        var veiculos = await _veiculoRepository.GetAllByUsuarioIdAsync(usuarioId, ct);

        return veiculos.Select(x => new VeiculoResponse(x)).ToList();
    }
}
