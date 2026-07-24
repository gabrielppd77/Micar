using Contracts.Repositories;
using Contracts.Repositories.Veiculos;
using Domain.Veiculos;

namespace Application.Veiculos.Create;

public class CreateVeiculoService
{
    private readonly IVeiculoRepository _veiculoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateVeiculoService(IVeiculoRepository veiculoRepository, IUnitOfWork unitOfWork)
    {
        _veiculoRepository = veiculoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(CreateVeiculoRequest request, CancellationToken ct)
    {
        var veiculo = new Veiculo(request.Placa, request.Apelido, request.TipoVeiculo);

        await _veiculoRepository.AddAsync(veiculo, ct);

        await _unitOfWork.SaveChangesAsync(ct);
    }
}
