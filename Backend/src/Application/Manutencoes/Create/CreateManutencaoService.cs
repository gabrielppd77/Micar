using Contracts.Authentications;
using Contracts.Repositories;
using Contracts.Repositories.Veiculos;
using Domain.Exceptions;

namespace Application.Manutencoes.Create;

public class CreateManutencaoService
{
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUsuarioService _currentUsuarioService;

    public CreateManutencaoService(
        IVeiculoRepository veiculoRepository,
        IUnitOfWork unitOfWork,
        ICurrentUsuarioService currentUsuarioService)
    {
        _veiculoRepository = veiculoRepository;
        _unitOfWork = unitOfWork;
        _currentUsuarioService = currentUsuarioService;
    }

    public async Task ExecuteAsync(CreateManutencaoRequest request, CancellationToken ct)
    {
        var usuarioId = _currentUsuarioService.GetUsuarioId();

        var veiculo = await _veiculoRepository.GetByIdAsync(request.VeiculoId, ct);

        if (veiculo is null || veiculo.UsuarioId != usuarioId)
            throw new NotFoundException("Veículo não encontrado.");

        veiculo.RegistrarManutencao(
            request.Data,
            request.Nome,
            request.Odometro,
            request.OdometroVencimento,
            request.DataVencimento,
            request.Valor);

        await _unitOfWork.SaveChangesAsync(ct);
    }
}
