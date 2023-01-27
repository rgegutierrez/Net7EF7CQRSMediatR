using MediatR;
using MediatrExample.ApplicationCore.Features.ValorFisicoPieMaquinaFeatures.Queries;
using MediatrExample.ApplicationCore.Features.ValorFisicoPieMaquinaFeatures.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.WebApi.Controllers;


[Authorize]
[ApiController]
[Route("api/valorfisicopiemaquina")]
public class ValorFisicoPieMaquinaController : ControllerBase
{
    private readonly IMediator _mediator;

    public ValorFisicoPieMaquinaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Consulta los valores fisicos
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<List<GetListValorFisicoPieMaquinaQueryResponse>> GetValorFisicoPieMaquina() => _mediator.Send(new GetListValorFisicoPieMaquinaQuery());

    /// <summary>
    /// Crea un valor fisico
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateValorFisicoPieMaquina([FromBody] CreateValorFisicoPieMaquinaCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Actualiza un valor fisico
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateValorFisicoPieMaquina([FromBody] UpdateValorFisicoPieMaquinaCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Consulta un valor fisico por su ID
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("{ValorFisicoPieMaquinaId}")]
    public Task<GetValorFisicoPieMaquinaQueryResponse> GetValorFisicoPieMaquinaById([FromRoute] GetValorFisicoPieMaquinaQuery query) =>
        _mediator.Send(query);

    /// <summary>
    /// nuevo registro valor fisico
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("new")]
    public Task<NewValorFisicoPieMaquinaQueryResponse> NewValorFisicoPieMaquina([FromRoute] NewValorFisicoPieMaquinaQuery query) =>
        _mediator.Send(query);
}
