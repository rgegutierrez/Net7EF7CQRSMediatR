using MediatR;
using MediatrExample.ApplicationCore.Features.IndicadorSecadorFeatures.Queries;
using MediatrExample.ApplicationCore.Features.IndicadorSecadorFeatures.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.WebApi.Controllers;


[Authorize]
[ApiController]
[Route("api/indicadorsecador")]
public class IndicadorSecadorController : ControllerBase
{
    private readonly IMediator _mediator;

    public IndicadorSecadorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Consulta los indicadores de secador
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<List<GetListIndicadorSecadorQueryResponse>> GetIndicadorSecador() => _mediator.Send(new GetListIndicadorSecadorQuery());

    /// <summary>
    /// Crea un indicador secador
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateIndicadorSecador([FromBody] CreateIndicadorSecadorCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Actualiza un indicador secador
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateIndicadorSecador([FromBody] UpdateIndicadorSecadorCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Consulta un indicador secador por su ID
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("{IndicadorSecadorId}")]
    public Task<GetIndicadorSecadorQueryResponse> GetIndicadorSecadorById([FromRoute] GetIndicadorSecadorQuery query) =>
        _mediator.Send(query);

    /// <summary>
    /// nuevo registro indicador secador
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("new")]
    public Task<NewIndicadorSecadorQueryResponse> NewIndicadorSecador([FromRoute] NewIndicadorSecadorQuery query) =>
        _mediator.Send(query);
}
