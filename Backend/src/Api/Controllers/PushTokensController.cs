using Application.PushTokens.Registrar;
using Application.PushTokens.Remover;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PushTokensController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Registrar(
        RegistrarPushTokenService registrarPushTokenService,
        RegistrarPushTokenRequest request,
        CancellationToken ct)
    {
        await registrarPushTokenService.ExecuteAsync(request, ct);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Remover(
        RemoverPushTokenService removerPushTokenService,
        [FromQuery] string token,
        CancellationToken ct)
    {
        await removerPushTokenService.ExecuteAsync(token, ct);
        return NoContent();
    }
}
