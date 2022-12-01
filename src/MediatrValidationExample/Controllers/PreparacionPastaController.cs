using MediatR;
using MediatrExample.ApplicationCore.Features.PreparacionPastaFeatures.Commands;
using MediatrExample.ApplicationCore.Features.PreparacionPastaFeatures.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.WebApi.Controllers;


[Authorize]
[ApiController]
[Route("api/preparacionpasta")]
public class PreparacionPastaController : ControllerBase
{
    private readonly IMediator _mediator;

    public PreparacionPastaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Consulta registros preparacion pasta
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<List<GetListPreparacionPastaQueryResponse>> GetPreparacionPasta() => _mediator.Send(new GetListPreparacionPastaQuery());

    /// <summary>
    /// Crea registro preparacion pasta
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreatePreparacionPasta([FromBody] CreatePreparacionPastaCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Actualiza registro preparacion pasta
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdatePreparacionPasta([FromBody] UpdatePreparacionPastaCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Consulta registro preparacion pasta por su ID
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("{PreparacionPastaId}")]
    public Task<GetPreparacionPastaQueryResponse> GetPreparacionPastaById([FromRoute] GetPreparacionPastaQuery query) =>
        _mediator.Send(query);
}
