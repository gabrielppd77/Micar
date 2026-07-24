namespace Application.Usuarios.Login;

public class LoginUsuarioRequest
{
    public required string Email { get; set; }
    public required string Senha { get; set; }
}
