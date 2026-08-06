using Application.RegistrosOdometro.Create;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrosOdometroController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateRegistroOdometroService createRegistroOdometroService,
        CreateRegistroOdometroRequest request,
        CancellationToken ct)
    {
        await createRegistroOdometroService.ExecuteAsync(request, ct);
        return NoContent();
    }
}
