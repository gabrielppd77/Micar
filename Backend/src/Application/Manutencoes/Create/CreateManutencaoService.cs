using Contracts.Authentications;
using Contracts.Repositories;
using Contracts.Repositories.Manutencoes;
using Contracts.Repositories.RegistrosOdometro;
using Contracts.Repositories.Veiculos;
using Domain.Exceptions;
using Domain.Manutencoes;
using Domain.RegistrosOdometro;

namespace Application.Manutencoes.Create;

public class CreateManutencaoService
{
    private readonly IManutencaoRepository _manutencaoRepository;
    private readonly IRegistroOdometroRepository _registroOdometroRepository;
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUsuarioService _currentUsuarioService;

    public CreateManutencaoService(
        IManutencaoRepository manutencaoRepository,
        IRegistroOdometroRepository registroOdometroRepository,
        IVeiculoRepository veiculoRepository,
        IUnitOfWork unitOfWork,
        ICurrentUsuarioService currentUsuarioService)
    {
        _manutencaoRepository = manutencaoRepository;
        _registroOdometroRepository = registroOdometroRepository;
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

        var manutencao = new Manutencao(
            request.Data,
            request.Nome,
            veiculo.Id,
            request.OdometroVencimento,
            request.DataVencimento);

        var registroOdometro = new RegistroOdometro(
            request.Data,
            request.Odometro,
            veiculo.Id,
            manutencao.Id);

        await _manutencaoRepository.AddAsync(manutencao, ct);

        await _registroOdometroRepository.AddAsync(registroOdometro, ct);

        await _unitOfWork.SaveChangesAsync(ct);
    }
}
