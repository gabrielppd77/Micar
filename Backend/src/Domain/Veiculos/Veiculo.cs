using Domain.Common;
using Domain.Exceptions;
using Domain.Manutencoes;
using Domain.RegistrosOdometro;
using Domain.Usuarios;

namespace Domain.Veiculos;

public class Veiculo : Entity
{
    public string Placa { get; private set; } = string.Empty;
    public string Apelido { get; private set; } = string.Empty;
    public TipoVeiculoEnum TipoVeiculo { get; private set; }
    public Guid UsuarioId { get; private set; }
    public Usuario? Usuario { get; private set; }
    public ICollection<RegistroOdometro> RegistrosOdometro { get; private set; } = new List<RegistroOdometro>();
    public ICollection<Manutencao> Manutencoes { get; private set; } = new List<Manutencao>();

    public const int PlacaLength = 7;

    private Veiculo()
    {
    }

    public Veiculo(string placa, string apelido, TipoVeiculoEnum tipoVeiculo, Guid usuarioId)
    {
        if (usuarioId == Guid.Empty)
            throw new BadRequestException("Usuário é obrigatório.");

        Placa = ValidarPlaca(placa);
        Apelido = ValidarApelido(apelido);
        TipoVeiculo = tipoVeiculo;
        UsuarioId = usuarioId;
    }

    public void Atualizar(string placa, string apelido, TipoVeiculoEnum tipoVeiculo)
    {
        Placa = ValidarPlaca(placa);
        Apelido = ValidarApelido(apelido);
        TipoVeiculo = tipoVeiculo;
    }

    public void AtualizarOdometroAtual(int odometro, DateOnly data)
    {
        var ultimoRegistroOdometro = RegistrosOdometro.FirstOrDefault();

        if (ultimoRegistroOdometro is not null)
            ultimoRegistroOdometro.Atualizar(data, odometro);
        else
            RegistrosOdometro.Add(new RegistroOdometro(data, odometro, Id));
    }

    private static string ValidarPlaca(string placa)
    {
        if (string.IsNullOrWhiteSpace(placa))
            throw new BadRequestException("Placa é obrigatória.");

        if (placa.Length != PlacaLength)
            throw new BadRequestException($"Placa deve ter exatamente {PlacaLength} caracteres.");

        return placa.ToUpper();
    }

    private static string ValidarApelido(string apelido)
    {
        if (string.IsNullOrWhiteSpace(apelido))
            throw new BadRequestException("Apelido é obrigatório.");

        return apelido;
    }
}
