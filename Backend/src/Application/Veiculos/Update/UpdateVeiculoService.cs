using Contracts.Authentications;
using Contracts.Repositories;
using Contracts.Repositories.RegistrosOdometro;
using Contracts.Repositories.Veiculos;
using Domain.Exceptions;
using Domain.RegistrosOdometro;

namespace Application.Veiculos.Update;

public class UpdateVeiculoService
{
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly IRegistroOdometroRepository _registroOdometroRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUsuarioService _currentUsuarioService;

    public UpdateVeiculoService(
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

    public async Task ExecuteAsync(Guid id, UpdateVeiculoRequest request, CancellationToken ct)
    {
        var usuarioId = _currentUsuarioService.GetUsuarioId();

        var veiculo = await _veiculoRepository.GetByIdAsync(id, ct);

        if (veiculo is null || veiculo.UsuarioId != usuarioId)
            throw new NotFoundException("Veículo não encontrado.");

        veiculo.Atualizar(request.Placa, request.Apelido, request.TipoVeiculo);

        if (request.Odometro.HasValue)
        {
            var ultimoRegistroOdometro = veiculo.RegistrosOdometro.FirstOrDefault();

            if (ultimoRegistroOdometro is not null)
            {
                ultimoRegistroOdometro.Atualizar(DateOnly.FromDateTime(DateTime.UtcNow), request.Odometro.Value);
            }
            else
            {
                var registroOdometro = new RegistroOdometro(
                    DateOnly.FromDateTime(DateTime.UtcNow),
                    request.Odometro.Value,
                    veiculo.Id);

                await _registroOdometroRepository.AddAsync(registroOdometro, ct);
            }
        }

        await _unitOfWork.SaveChangesAsync(ct);
    }
}
