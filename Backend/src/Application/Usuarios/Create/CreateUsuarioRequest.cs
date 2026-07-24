namespace Application.Usuarios.Create;

public class CreateUsuarioRequest
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}
