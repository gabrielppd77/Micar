using Contracts.Authentications;
using Contracts.Repositories.Manutencoes;
using Contracts.Repositories.Veiculos;
using Domain.Common;
using Domain.Exceptions;

namespace Application.Manutencoes.GetStatus;

public class GetStatusManutencoesVeiculoService
{
    private readonly IManutencaoRepository _manutencaoRepository;
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly ICurrentUsuarioService _currentUsuarioService;

    public GetStatusManutencoesVeiculoService(
        IManutencaoRepository manutencaoRepository,
        IVeiculoRepository veiculoRepository,
        ICurrentUsuarioService currentUsuarioService)
    {
        _manutencaoRepository = manutencaoRepository;
        _veiculoRepository = veiculoRepository;
        _currentUsuarioService = currentUsuarioService;
    }

    public async Task<VeiculoStatusManutencaoResponse> ExecuteAsync(Guid veiculoId, CancellationToken ct)
    {
        var usuarioId = _currentUsuarioService.GetUsuarioId();

        var veiculo = await _veiculoRepository.GetByIdAsync(veiculoId, ct);

        if (veiculo is null || veiculo.UsuarioId != usuarioId)
            throw new NotFoundException("Veículo não encontrado.");

        var manutencoes = await _manutencaoRepository.GetAllByVeiculoIdAsync(veiculoId, ct);

        var hoje = DateOnly.FromDateTime(DateTime.UtcNow);
        var odometroAtual = veiculo.UltimoRegistroOdometro?.Odometro;

        var todas = manutencoes
            .Where(m => m.DataConclusao is null)
            .Select(m => new ManutencaoPendenciaResponse(m, hoje, odometroAtual))
            .ToList();

        var pendencias = todas
            .Where(p => p.Status != NivelAlertaEnum.Normal)
            .OrderByDescending(p => p.Status)
            .ThenBy(p => p.DataVencimento ?? DateOnly.MaxValue)
            .ThenBy(p => p.OdometroVencimento ?? int.MaxValue)
            .ToList();

        return new VeiculoStatusManutencaoResponse(todas, pendencias);
    }
}
