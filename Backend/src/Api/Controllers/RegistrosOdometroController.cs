using Application.RegistrosOdometro.Common;
using Application.RegistrosOdometro.Create;
using Application.RegistrosOdometro.Delete;
using Application.RegistrosOdometro.GetAll;
using Application.RegistrosOdometro.GetById;
using Application.RegistrosOdometro.Update;
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

    [HttpGet("veiculo/{veiculoId:guid}")]
    public async Task<ActionResult<List<RegistroOdometroResponse>>> GetAll(
        GetAllRegistrosOdometroService getAllRegistrosOdometroService,
        Guid veiculoId,
        CancellationToken ct)
    {
        var registrosOdometro = await getAllRegistrosOdometroService.ExecuteAsync(veiculoId, ct);
        return Ok(registrosOdometro);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RegistroOdometroResponse>> GetById(
        GetRegistroOdometroByIdService getRegistroOdometroByIdService,
        Guid id,
        CancellationToken ct)
    {
        var registroOdometro = await getRegistroOdometroByIdService.ExecuteAsync(id, ct);
        return Ok(registroOdometro);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        UpdateRegistroOdometroService updateRegistroOdometroService,
        Guid id,
        UpdateRegistroOdometroRequest request,
        CancellationToken ct)
    {
        await updateRegistroOdometroService.ExecuteAsync(id, request, ct);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        DeleteRegistroOdometroService deleteRegistroOdometroService,
        Guid id,
        CancellationToken ct)
    {
        await deleteRegistroOdometroService.ExecuteAsync(id, ct);
        return NoContent();
    }
}
