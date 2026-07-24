using Application.Veiculos.Common;
using Application.Veiculos.Create;
using Application.Veiculos.Delete;
using Application.Veiculos.GetAll;
using Application.Veiculos.GetById;
using Application.Veiculos.Update;
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

    [HttpGet]
    public async Task<ActionResult<List<VeiculoResponse>>> GetAll(
        GetAllVeiculosService getAllVeiculosService,
        CancellationToken ct)
    {
        var veiculos = await getAllVeiculosService.ExecuteAsync(ct);
        return Ok(veiculos);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<VeiculoResponse>> GetById(
        GetVeiculoByIdService getVeiculoByIdService,
        Guid id,
        CancellationToken ct)
    {
        var veiculo = await getVeiculoByIdService.ExecuteAsync(id, ct);
        return Ok(veiculo);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        UpdateVeiculoService updateVeiculoService,
        Guid id,
        UpdateVeiculoRequest request,
        CancellationToken ct)
    {
        await updateVeiculoService.ExecuteAsync(id, request, ct);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        DeleteVeiculoService deleteVeiculoService,
        Guid id,
        CancellationToken ct)
    {
        await deleteVeiculoService.ExecuteAsync(id, ct);
        return NoContent();
    }
}
