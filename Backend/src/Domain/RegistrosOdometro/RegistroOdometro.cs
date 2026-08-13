using Domain.Common;
using Domain.Exceptions;
using Domain.Manutencoes;
using Domain.Veiculos;

namespace Domain.RegistrosOdometro;

public class RegistroOdometro : Entity
{
    public DateOnly Data { get; private set; }
    public int Odometro { get; private set; }
    public Guid VeiculoId { get; private set; }
    public Veiculo? Veiculo { get; private set; }
    public Guid? ManutencaoId { get; private set; }
    public Manutencao? Manutencao { get; private set; }
    private const int LimiteDiasAtencao = 15;
    private const int LimiteDiasDesatualizado = 30;

    private RegistroOdometro()
    {
    }

    public RegistroOdometro(DateOnly data, int odometro, Guid veiculoId, Guid? manutencaoId = null)
    {
        if (veiculoId == Guid.Empty)
            throw new BadRequestException("Veículo é obrigatório.");

        Data = ValidarData(data);
        Odometro = ValidarOdometro(odometro);
        VeiculoId = veiculoId;
        ManutencaoId = manutencaoId;
    }

    public void Atualizar(DateOnly data, int odometro)
    {
        Data = ValidarData(data);
        Odometro = ValidarOdometro(odometro);
    }

    public int CalcularDiasSemAtualizacao(DateOnly hoje)
    {
        return hoje.DayNumber - Data.DayNumber;
    }

    public NivelAlertaEnum CalcularStatus(DateOnly hoje)
    {
        var dias = CalcularDiasSemAtualizacao(hoje);

        if (dias >= LimiteDiasDesatualizado)
            return NivelAlertaEnum.Critico;

        return dias >= LimiteDiasAtencao ? NivelAlertaEnum.Atencao : NivelAlertaEnum.Normal;
    }

    private static DateOnly ValidarData(DateOnly data)
    {
        if (data == default)
            throw new BadRequestException("Data é obrigatória.");

        return data;
    }

    private static int ValidarOdometro(int odometro)
    {
        if (odometro < 0)
            throw new BadRequestException("Odômetro deve ser maior ou igual a zero.");

        return odometro;
    }
}
