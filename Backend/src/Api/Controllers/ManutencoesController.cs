using Application.Manutencoes.Common;
using Application.Manutencoes.Create;
using Application.Manutencoes.Delete;
using Application.Manutencoes.GetAll;
using Application.Manutencoes.GetById;
using Application.Manutencoes.GetStatus;
using Application.Manutencoes.Update;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ManutencoesController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        CreateManutencaoService createManutencaoService,
        CreateManutencaoRequest request,
        CancellationToken ct)
    {
        await createManutencaoService.ExecuteAsync(request, ct);
        return NoContent();
    }

    [HttpGet("veiculo/{veiculoId:guid}")]
    public async Task<ActionResult<List<ManutencaoResponse>>> GetAll(
        GetAllManutencoesService getAllManutencoesService,
        Guid veiculoId,
        CancellationToken ct)
    {
        var manutencoes = await getAllManutencoesService.ExecuteAsync(veiculoId, ct);
        return Ok(manutencoes);
    }

    [HttpGet("veiculo/{veiculoId:guid}/status")]
    public async Task<ActionResult<VeiculoStatusManutencaoResponse>> GetStatus(
        GetStatusManutencoesVeiculoService getStatusManutencoesVeiculoService,
        Guid veiculoId,
        CancellationToken ct)
    {
        var status = await getStatusManutencoesVeiculoService.ExecuteAsync(veiculoId, ct);
        return Ok(status);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ManutencaoResponse>> GetById(
        GetManutencaoByIdService getManutencaoByIdService,
        Guid id,
        CancellationToken ct)
    {
        var manutencao = await getManutencaoByIdService.ExecuteAsync(id, ct);
        return Ok(manutencao);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        UpdateManutencaoService updateManutencaoService,
        Guid id,
        UpdateManutencaoRequest request,
        CancellationToken ct)
    {
        await updateManutencaoService.ExecuteAsync(id, request, ct);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        DeleteManutencaoService deleteManutencaoService,
        Guid id,
        CancellationToken ct)
    {
        await deleteManutencaoService.ExecuteAsync(id, ct);
        return NoContent();
    }
}
