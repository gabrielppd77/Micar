using Application.Veiculos.Create;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VeiculosController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateVeiculoService createVeiculoService,
        CreateVeiculoRequest request,
        CancellationToken ct)
    {
        await createVeiculoService.ExecuteAsync(request, ct);
        return NoContent();
    }
}
