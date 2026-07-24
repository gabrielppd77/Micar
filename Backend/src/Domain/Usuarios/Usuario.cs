using Domain.Common;
using Domain.Exceptions;
using Domain.Veiculos;

namespace Domain.Usuarios;

public class Usuario : Entity
{
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Senha { get; private set; } = string.Empty;
    public ICollection<Veiculo> Veiculos { get; private set; } = new List<Veiculo>();

    private Usuario()
    {
    }

    public Usuario(string nome, string email, string senha)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new BadRequestException("Nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new BadRequestException("Email é obrigatório e deve ser válido.");

        if (string.IsNullOrWhiteSpace(senha))
            throw new BadRequestException("Senha é obrigatória.");

        Nome = nome;
        Email = email;
        Senha = senha;
    }
}
