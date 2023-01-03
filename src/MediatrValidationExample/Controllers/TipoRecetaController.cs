using MediatR;
using MediatrExample.ApplicationCore.Features.TipoRecetaFeatures.Commands;
using MediatrExample.ApplicationCore.Features.TipoRecetaFeatures.Queries;
using MediatrExample.ApplicationCore.Features.PreparacionPastaFeatures.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.WebApi.Controllers;


[Authorize]
[ApiController]
[Route("api/tiporeceta")]
public class TipoRecetaController : ControllerBase
{
    private readonly IMediator _mediator;

    public TipoRecetaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Consulta registros tipo de receta
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<List<GetListTipoRecetaQueryResponse>> GetTipoReceta() => _mediator.Send(new GetListTipoRecetaQuery());

    /// <summary>
    /// Crea registro tipo de receta
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateTipoReceta([FromBody] CreateTipoRecetaCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Actualiza registro tipo de receta
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateTipoReceta([FromBody] UpdateTipoRecetaCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Consulta registro tipo de receta por su ID
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("{TipoRecetaId}")]
    public Task<GetTipoRecetaQueryResponse> GetTipoRecetaById([FromRoute] GetTipoRecetaQuery query) =>
        _mediator.Send(query);

    /// <summary>
    /// nuevo registro tipo de receta
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("new")]
    public Task<NewTipoRecetaQueryResponse> NewTipoReceta([FromRoute] NewTipoRecetaQuery query) =>
        _mediator.Send(query);
}
