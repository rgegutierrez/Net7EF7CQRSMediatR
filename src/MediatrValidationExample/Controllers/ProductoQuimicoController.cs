using MediatR;
using MediatrExample.ApplicationCore.Features.ProductoQuimicoFeatures.Commands;
using MediatrExample.ApplicationCore.Features.ProductoQuimicoFeatures.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.WebApi.Controllers;


[Authorize]
[ApiController]
[Route("api/productoquimico")]
public class ProductoQuimicoController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductoQuimicoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Consulta los productos quimicos
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<List<GetListProductoQuimicoQueryResponse>> GetProductoQuimico() => _mediator.Send(new GetListProductoQuimicoQuery());

    /// <summary>
    /// Crea un producto quimico nuevo
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateProductoQuimico([FromBody] UpdateProductoQuimicoCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// Consulta un producto quimico por su ID
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("{ProductoQuimicoId}")]
    public Task<GetProductoQuimicoQueryResponse> GetProductoQuimicoById([FromRoute] GetProductoQuimicoQuery query) =>
        _mediator.Send(query);
}
