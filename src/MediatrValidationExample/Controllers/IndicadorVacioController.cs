using MediatR;
using MediatrExample.ApplicationCore.Features.IndicadorVacioFeatures.Queries;
using MediatrExample.ApplicationCore.Features.IndicadorVacioFeatures.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.WebApi.Controllers;


[Authorize]
[ApiController]
[Route("api/indicadorvacio")]
public class IndicadorVacioController : ControllerBase
{
    private readonly IMediator _mediator;

    public IndicadorVacioController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Consulta los indicadores de vacio
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<List<GetListIndicadorVacioQueryResponse>> GetIndicadorVacio() => _mediator.Send(new GetListIndicadorVacioQuery());

    /// <summary>
    /// Crea un indicador vacio
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateIndicadorVacio([FromBody] CreateIndicadorVacioCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Actualiza un indicador vacio
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateIndicadorVacio([FromBody] UpdateIndicadorVacioCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Consulta un indicador vacio por su ID
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("{IndicadorVacioId}")]
    public Task<GetIndicadorVacioQueryResponse> GetIndicadorVacioById([FromRoute] GetIndicadorVacioQuery query) =>
        _mediator.Send(query);

    /// <summary>
    /// nuevo registro indicador vacio
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("new")]
    public Task<NewIndicadorVacioQueryResponse> NewIndicadorVacio([FromRoute] NewIndicadorVacioQuery query) =>
        _mediator.Send(query);
}
