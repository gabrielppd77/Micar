using Contracts.Authentications;
using Contracts.Repositories;
using Contracts.Repositories.Veiculos;
using Domain.Veiculos;

namespace Application.Veiculos.Create;

public class CreateVeiculoService
{
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public CreateVeiculoService(
        IVeiculoRepository veiculoRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService)
    {
        _veiculoRepository = veiculoRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task ExecuteAsync(CreateVeiculoRequest request, CancellationToken ct)
    {
        var userId = _currentUserService.GetUserId();

        var veiculo = new Veiculo(request.Placa, request.Apelido, request.TipoVeiculo, userId);

        await _veiculoRepository.AddAsync(veiculo, ct);

        await _unitOfWork.SaveChangesAsync(ct);
    }
}
