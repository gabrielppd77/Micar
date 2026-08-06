using Contracts.Authentications;
using Contracts.Repositories;
using Contracts.Repositories.RegistrosOdometro;
using Contracts.Repositories.Veiculos;
using Domain.RegistrosOdometro;
using Domain.Veiculos;

namespace Application.Veiculos.Create;

public class CreateVeiculoService
{
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly IRegistroOdometroRepository _registroOdometroRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUsuarioService _currentUsuarioService;

    public CreateVeiculoService(
        IVeiculoRepository veiculoRepository,
        IRegistroOdometroRepository registroOdometroRepository,
        IUnitOfWork unitOfWork,
        ICurrentUsuarioService currentUsuarioService)
    {
        _veiculoRepository = veiculoRepository;
        _registroOdometroRepository = registroOdometroRepository;
        _unitOfWork = unitOfWork;
        _currentUsuarioService = currentUsuarioService;
    }

    public async Task ExecuteAsync(CreateVeiculoRequest request, CancellationToken ct)
    {
        var usuarioId = _currentUsuarioService.GetUsuarioId();

        var veiculo = new Veiculo(request.Placa, request.Apelido, request.TipoVeiculo, usuarioId);

        await _veiculoRepository.AddAsync(veiculo, ct);

        if (request.Odometro.HasValue)
        {
            var registroOdometro = new RegistroOdometro(
                DateOnly.FromDateTime(DateTime.UtcNow),
                request.Odometro.Value,
                veiculo.Id);

            await _registroOdometroRepository.AddAsync(registroOdometro, ct);
        }

        await _unitOfWork.SaveChangesAsync(ct);
    }
}
