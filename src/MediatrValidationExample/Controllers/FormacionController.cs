using MediatR;
using MediatrExample.ApplicationCore.Features.FormacionFeatures.Commands;
using MediatrExample.ApplicationCore.Features.FormacionFeatures.Queries;
using MediatrExample.ApplicationCore.Features.PreparacionPastaFeatures.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.WebApi.Controllers;


[Authorize]
[ApiController]
[Route("api/formacion")]
public class FormacionController : ControllerBase
{
    private readonly IMediator _mediator;

    public FormacionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Consulta registros formación
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<List<GetListFormacionQueryResponse>> GetFormacion() => _mediator.Send(new GetListFormacionQuery());

    /// <summary>
    /// Crea registro formación
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateFormacion([FromBody] CreateFormacionCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Actualiza registro formación
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateFormacion([FromBody] UpdateFormacionCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Consulta registro formación por su ID
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("{FormacionId}")]
    public Task<GetFormacionQueryResponse> GetFormacionById([FromRoute] GetFormacionQuery query) =>
        _mediator.Send(query);

    /// <summary>
    /// nuevo registro formación
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("new")]
    public Task<NewFormacionQueryResponse> NewFormacion([FromRoute] NewFormacionQuery query) =>
        _mediator.Send(query);
}
