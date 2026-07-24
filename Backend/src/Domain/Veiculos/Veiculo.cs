using Domain.Common;
using Domain.Exceptions;
using Domain.Users;

namespace Domain.Veiculos;

public class Veiculo : Entity
{
    public string Placa { get; private set; } = string.Empty;
    public string Apelido { get; private set; } = string.Empty;
    public TipoVeiculoEnum TipoVeiculo { get; private set; }
    public Guid UserId { get; private set; }
    public User? User { get; private set; }

    public const int PlacaLength = 7;

    private Veiculo()
    {
    }

    public Veiculo(string placa, string apelido, TipoVeiculoEnum tipoVeiculo, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(placa))
            throw new BadRequestException("Placa é obrigatória.");

        if (placa.Length != PlacaLength)
            throw new BadRequestException($"Placa deve ter exatamente {PlacaLength} caracteres.");

        if (string.IsNullOrWhiteSpace(apelido))
            throw new BadRequestException("Apelido é obrigatório.");

        if (userId == Guid.Empty)
            throw new BadRequestException("Usuário é obrigatório.");

        Placa = placa.ToUpper();
        Apelido = apelido;
        TipoVeiculo = tipoVeiculo;
        UserId = userId;
    }
}
