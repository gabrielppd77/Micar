using Application.RegistrosOdometro.Create;
using Application.RegistrosOdometro.GetStatus;
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

    [HttpGet("veiculo/{veiculoId:guid}/status")]
    public async Task<ActionResult<OdometroStatusResponse>> GetStatus(
        GetStatusOdometroVeiculoService getStatusOdometroVeiculoService,
        Guid veiculoId,
        CancellationToken ct)
    {
        var status = await getStatusOdometroVeiculoService.ExecuteAsync(veiculoId, ct);
        return Ok(status);
    }
}
