using Contracts.Authentications;
using Contracts.Repositories;
using Contracts.Repositories.Veiculos;
using Domain.Exceptions;

namespace Application.RegistrosOdometro.Create;

public class CreateRegistroOdometroService
{
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUsuarioService _currentUsuarioService;

    public CreateRegistroOdometroService(
        IVeiculoRepository veiculoRepository,
        IUnitOfWork unitOfWork,
        ICurrentUsuarioService currentUsuarioService)
    {
        _veiculoRepository = veiculoRepository;
        _unitOfWork = unitOfWork;
        _currentUsuarioService = currentUsuarioService;
    }

    public async Task ExecuteAsync(CreateRegistroOdometroRequest request, CancellationToken ct)
    {
        var usuarioId = _currentUsuarioService.GetUsuarioId();

        var veiculo = await _veiculoRepository.GetByIdAsync(request.VeiculoId, ct);

        if (veiculo is null || veiculo.UsuarioId != usuarioId)
            throw new NotFoundException("Veículo não encontrado.");

        var data = DateOnly.FromDateTime(DateTime.UtcNow);
        veiculo.RegistrarOdometro(request.Odometro, data);

        await _unitOfWork.SaveChangesAsync(ct);
    }
}
