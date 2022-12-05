using MediatR;
using MediatrExample.ApplicationCore.Features.MaquinaPapeleraFeatures.Commands;
using MediatrExample.ApplicationCore.Features.MaquinaPapeleraFeatures.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.WebApi.Controllers;


[Authorize]
[ApiController]
[Route("api/maquinapapelera")]
public class MaquinaPapeleraController : ControllerBase
{
    private readonly IMediator _mediator;

    public MaquinaPapeleraController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Consulta registros maquina papelera
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<List<GetListMaquinaPapeleraQueryResponse>> GetMaquinaPapelera() => _mediator.Send(new GetListMaquinaPapeleraQuery());

    /// <summary>
    /// Crea registro maquina papelera
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateMaquinaPapelera([FromBody] CreateMaquinaPapeleraCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Actualiza registro maquina papelera
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateMaquinaPapelera([FromBody] UpdateMaquinaPapeleraCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Actualiza orden lista maquina papelera
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("UpdateOrden")]
    public async Task<IActionResult> UpdateOrden([FromBody] UpdateOrdenCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Consulta registro maquina papelera por ID
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("{MaquinaPapeleraId}")]
    public Task<GetMaquinaPapeleraQueryResponse> GetMaquinaPapeleraById([FromRoute] GetMaquinaPapeleraQuery query) =>
        _mediator.Send(query);
}
