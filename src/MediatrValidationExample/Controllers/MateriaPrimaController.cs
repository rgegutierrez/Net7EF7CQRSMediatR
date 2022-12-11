using MediatR;
using MediatrExample.ApplicationCore.Features.MateriaPrimaFeatures.Commands;
using MediatrExample.ApplicationCore.Features.MateriaPrimaFeatures.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.WebApi.Controllers;


[Authorize]
[ApiController]
[Route("api/materiaprima")]
public class MateriaPrimaController : ControllerBase
{
    private readonly IMediator _mediator;

    public MateriaPrimaController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Consulta los productos
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<List<GetListMateriaPrimaQueryResponse>> GetMateriaPrima() => _mediator.Send(new GetListMateriaPrimaQuery());

    /// <summary>
    /// Crea un producto nuevo
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateMateriaPrima([FromBody] UpdateMateriaPrimaCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Consulta un producto por su ID
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("{MateriaPrimaId}")]
    public Task<GetMateriaPrimaQueryResponse> GetMateriaPrimaById([FromRoute] GetPreparacionPastaQuery query) =>
        _mediator.Send(query);
}
