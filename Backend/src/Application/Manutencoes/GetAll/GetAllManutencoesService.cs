using Application.Manutencoes.Common;
using Contracts.Authentications;
using Contracts.Repositories.Manutencoes;
using Contracts.Repositories.Veiculos;
using Domain.Exceptions;

namespace Application.Manutencoes.GetAll;

public class GetAllManutencoesService
{
    private readonly IManutencaoRepository _manutencaoRepository;
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly ICurrentUsuarioService _currentUsuarioService;

    public GetAllManutencoesService(
        IManutencaoRepository manutencaoRepository,
        IVeiculoRepository veiculoRepository,
        ICurrentUsuarioService currentUsuarioService)
    {
        _manutencaoRepository = manutencaoRepository;
        _veiculoRepository = veiculoRepository;
        _currentUsuarioService = currentUsuarioService;
    }

    public async Task<List<ManutencaoResponse>> ExecuteAsync(Guid veiculoId, CancellationToken ct)
    {
        var usuarioId = _currentUsuarioService.GetUsuarioId();

        var veiculo = await _veiculoRepository.GetByIdAsync(veiculoId, ct);

        if (veiculo is null || veiculo.UsuarioId != usuarioId)
            throw new NotFoundException("Veículo não encontrado.");

        var manutencoes = await _manutencaoRepository.GetAllByVeiculoIdAsync(veiculoId, ct);

        return manutencoes.Select(x => new ManutencaoResponse(x)).ToList();
    }
}
