using Domain.Common;
using Domain.Exceptions;

namespace Domain.Users;

public class User : Entity
{
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;

    private User()
    {
    }

    public User(string name, string email, string password)
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
