using Application.Usuarios.Create;
using Application.Usuarios.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuariosController : ControllerBase
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateUsuarioService createUsuarioService,
        CreateUsuarioRequest request,
        CancellationToken ct)
    {
        var response = await createUsuarioService.ExecuteAsync(request, ct);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        LoginUsuarioService loginUsuarioService,
        LoginUsuarioRequest request,
        CancellationToken ct)
    {
        var response = await loginUsuarioService.ExecuteAsync(request, ct);
        return Ok(response);
    }
}
