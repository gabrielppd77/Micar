using Domain.Common;
using Domain.Exceptions;
using Domain.Veiculos;

namespace Domain.Usuarios;

public class Usuario : Entity
{
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public ICollection<Veiculo> Veiculos { get; private set; } = new List<Veiculo>();

    private Usuario()
    {
    }

    public Usuario(string name, string email, string password)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BadRequestException("Nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new BadRequestException("Email é obrigatório e deve ser válido.");

        if (string.IsNullOrWhiteSpace(password))
            throw new BadRequestException("Senha é obrigatória.");

        Name = name;
        Email = email;
        Password = password;
    }
}
