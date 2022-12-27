using MediatR;
using MediatrExample.ApplicationCore.Features.LineaProduccionFeatures.Commands;
using MediatrExample.ApplicationCore.Features.LineaProduccionFeatures.Queries;
using MediatrExample.ApplicationCore.Features.PreparacionPastaFeatures.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.WebApi.Controllers;


[Authorize]
[ApiController]
[Route("api/lineaproduccion")]
public class LineaProduccionController : ControllerBase
{
    private readonly IMediator _mediator;

    public LineaProduccionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Consulta registros linea producción
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<List<GetListLineaProduccionQueryResponse>> GetLineaProduccion() => _mediator.Send(new GetListLineaProduccionQuery());

    /// <summary>
    /// Crea registro linea producción
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateLineaProduccion([FromBody] CreateLineaProduccionCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Actualiza registro linea producción
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateLineaProduccion([FromBody] UpdateLineaProduccionCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Consulta registro linea producción por su ID
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("{LineaProduccionId}")]
    public Task<GetLineaProduccionQueryResponse> GetLineaProduccionById([FromRoute] GetLineaProduccionQuery query) =>
        _mediator.Send(query);

    /// <summary>
    /// nuevo registro linea producción por su ID
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("new")]
    public Task<NewLineaProduccionQueryResponse> NewLineaProduccion([FromRoute] NewLineaProduccionQuery query) =>
        _mediator.Send(query);
}
