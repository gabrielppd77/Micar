using Application.Veiculos.Common;
using Contracts.Authentications;
using Contracts.Repositories.Veiculos;
using Domain.Exceptions;

namespace Application.Veiculos.GetById;

public class GetVeiculoByIdService
{
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly ICurrentUsuarioService _currentUsuarioService;

    public GetVeiculoByIdService(
        IVeiculoRepository veiculoRepository,
        ICurrentUsuarioService currentUsuarioService)
    {
        _veiculoRepository = veiculoRepository;
        _currentUsuarioService = currentUsuarioService;
    }

    public async Task<VeiculoResponse> ExecuteAsync(Guid id, CancellationToken ct)
    {
        var usuarioId = _currentUsuarioService.GetUsuarioId();

        var veiculo = await _veiculoRepository.GetByIdAsync(id, ct);

        if (veiculo is null || veiculo.UsuarioId != usuarioId)
            throw new NotFoundException("Veículo não encontrado.");

        return new VeiculoResponse(veiculo);
    }
}
