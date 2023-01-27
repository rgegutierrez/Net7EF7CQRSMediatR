using MediatR;
using MediatrExample.ApplicationCore.Features.IndicadorPrensaFeatures.Queries;
using MediatrExample.ApplicationCore.Features.IndicadorPrensaFeatures.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.WebApi.Controllers;


[Authorize]
[ApiController]
[Route("api/indicadorprensa")]
public class IndicadorPrensaController : ControllerBase
{
    private readonly IMediator _mediator;

    public IndicadorPrensaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Consulta los indicadores de prensa
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<List<GetListIndicadorPrensaQueryResponse>> GetIndicadorPrensa() => _mediator.Send(new GetListIndicadorPrensaQuery());

    /// <summary>
    /// Crea un indicador prensa
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateIndicadorPrensa([FromBody] CreateIndicadorPrensaCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Actualiza un indicador prensa
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateIndicadorPrensa([FromBody] UpdateIndicadorPrensaCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Consulta un indicador prensa por su ID
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("{IndicadorPrensaId}")]
    public Task<GetIndicadorPrensaQueryResponse> GetIndicadorPrensaById([FromRoute] GetIndicadorPrensaQuery query) =>
        _mediator.Send(query);

    /// <summary>
    /// nuevo registro indicador prensa
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("new")]
    public Task<NewIndicadorPrensaQueryResponse> NewIndicadorPrensa([FromRoute] NewIndicadorPrensaQuery query) =>
        _mediator.Send(query);
}
