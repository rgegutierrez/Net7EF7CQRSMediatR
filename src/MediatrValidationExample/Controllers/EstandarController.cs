using MediatR;
using MediatrExample.ApplicationCore.Features.EstandarFeatures.Queries;
using MediatrExample.ApplicationCore.Features.EstandarFeatures.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.WebApi.Controllers;


[Authorize]
[ApiController]
[Route("api/estandar")]
public class EstandarController : ControllerBase
{
    private readonly IMediator _mediator;

    public EstandarController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Consulta los estandares
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<List<GetListEstandarQueryResponse>> GetEstandar() => _mediator.Send(new GetListEstandarQuery());

    /// <summary>
    /// Crea un estándar
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateEstandar([FromBody] CreateEstandarCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Actualiza un estándar
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateEstandar([FromBody] UpdateEstandarCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Consulta un estándar por su ID
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("{EstandarId}")]
    public Task<GetEstandarQueryResponse> GetEstandarById([FromRoute] GetEstandarQuery query) =>
        _mediator.Send(query);

    /// <summary>
    /// nuevo registro estándar
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("new")]
    public Task<NewEstandarQueryResponse> NewEstandar([FromRoute] NewEstandarQuery query) =>
        _mediator.Send(query);
}
