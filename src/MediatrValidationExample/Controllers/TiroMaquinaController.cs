using MediatR;
using MediatrExample.ApplicationCore.Features.TiroMaquinaFeatures.Queries;
using MediatrExample.ApplicationCore.Features.TiroMaquinaFeatures.Commands;
using MediatrExample.ApplicationCore.Features.TiroMaquinaFeatures.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.WebApi.Controllers;


[Authorize]
[ApiController]
[Route("api/tiromaquina")]
public class TiroMaquinaController : ControllerBase
{
    private readonly IMediator _mediator;

    public TiroMaquinaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Consulta los tiros de máquina
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<List<GetListTiroMaquinaQueryResponse>> GetTiroMaquina() => _mediator.Send(new GetListTiroMaquinaQuery());

    /// <summary>
    /// Crea un tiro de máquina
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateTiroMaquina([FromBody] CreateTiroMaquinaCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Actualiza un tiro de máquina
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateTiroMaquina([FromBody] UpdateTiroMaquinaCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Consulta un tiro de máquina por su ID
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("{TiroMaquinaId}")]
    public Task<GetTiroMaquinaQueryResponse> GetTiroMaquinaById([FromRoute] GetTiroMaquinaQuery query) =>
        _mediator.Send(query);

    /// <summary>
    /// nuevo registro tiro de máquina
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("new")]
    public Task<NewTiroMaquinaQueryResponse> NewTiroMaquina([FromRoute] NewTiroMaquinaQuery query) =>
        _mediator.Send(query);
}
