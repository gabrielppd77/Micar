using Contracts.Authentications;
using Contracts.Repositories;
using Contracts.Repositories.RegistrosOdometro;
using Contracts.Repositories.Veiculos;
using Domain.Exceptions;
using Domain.RegistrosOdometro;

namespace Application.RegistrosOdometro.Create;

public class CreateRegistroOdometroService
{
    private readonly IRegistroOdometroRepository _registroOdometroRepository;
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUsuarioService _currentUsuarioService;

    public CreateRegistroOdometroService(
        IRegistroOdometroRepository registroOdometroRepository,
        IVeiculoRepository veiculoRepository,
        IUnitOfWork unitOfWork,
        ICurrentUsuarioService currentUsuarioService)
    {
        _registroOdometroRepository = registroOdometroRepository;
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

        var registroOdometro = new RegistroOdometro(request.Data, request.Odometro, veiculo.Id);

        await _registroOdometroRepository.AddAsync(registroOdometro, ct);

        await _unitOfWork.SaveChangesAsync(ct);
    }
}
