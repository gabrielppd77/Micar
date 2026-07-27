using Domain.Common;
using Domain.Exceptions;
using Domain.RegistrosOdometro;
using Domain.Veiculos;

namespace Domain.Manutencoes;

public class Manutencao : Entity
{
    public DateOnly Data { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public Guid VeiculoId { get; private set; }
    public Veiculo? Veiculo { get; private set; }
    public RegistroOdometro? RegistroOdometro { get; private set; }
    public int? OdometroVencimento { get; private set; }
    public DateOnly? DataVencimento { get; private set; }

    private Manutencao()
    {
    }

    public Manutencao(DateOnly data, string nome, Guid veiculoId, int? odometroVencimento, DateOnly? dataVencimento)
    {
        if (veiculoId == Guid.Empty)
            throw new BadRequestException("Veículo é obrigatório.");

        Data = ValidarData(data);
        Nome = ValidarNome(nome);
        VeiculoId = veiculoId;
        OdometroVencimento = ValidarOdometroVencimento(odometroVencimento);
        DataVencimento = dataVencimento;
    }

    public void Atualizar(DateOnly data, string nome, int? odometroVencimento, DateOnly? dataVencimento)
    {
        Data = ValidarData(data);
        Nome = ValidarNome(nome);
        OdometroVencimento = ValidarOdometroVencimento(odometroVencimento);
        DataVencimento = dataVencimento;
    }

    private static DateOnly ValidarData(DateOnly data)
    {
        if (data == default)
            throw new BadRequestException("Data é obrigatória.");

        return data;
    }

    private static string ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new BadRequestException("Nome é obrigatório.");

        return nome;
    }

    private static int? ValidarOdometroVencimento(int? odometroVencimento)
    {
        if (odometroVencimento is < 0)
            throw new BadRequestException("Odômetro de vencimento deve ser maior ou igual a zero.");

        return odometroVencimento;
    }
}
