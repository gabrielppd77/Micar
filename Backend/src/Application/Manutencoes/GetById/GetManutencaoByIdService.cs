using Application.Manutencoes.Common;
using Contracts.Authentications;
using Contracts.Repositories.Manutencoes;
using Domain.Exceptions;

namespace Application.Manutencoes.GetById;

public class GetManutencaoByIdService
{
    private readonly IManutencaoRepository _manutencaoRepository;
    private readonly ICurrentUsuarioService _currentUsuarioService;

    public GetManutencaoByIdService(
        IManutencaoRepository manutencaoRepository,
        ICurrentUsuarioService currentUsuarioService)
    {
        _manutencaoRepository = manutencaoRepository;
        _currentUsuarioService = currentUsuarioService;
    }

    public async Task<ManutencaoResponse> ExecuteAsync(Guid id, CancellationToken ct)
    {
        var usuarioId = _currentUsuarioService.GetUsuarioId();

        var manutencao = await _manutencaoRepository.GetByIdAsync(id, ct);

        if (manutencao is null || manutencao.Veiculo?.UsuarioId != usuarioId)
            throw new NotFoundException("Manutenção não encontrada.");

        return new ManutencaoResponse(manutencao);
    }
}
