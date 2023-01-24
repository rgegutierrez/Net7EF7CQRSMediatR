using MediatR;
using MediatrExample.ApplicationCore.Features.Products.Queries;
using MediatrExample.ApplicationCore.Features.RecetaFabricacionFeatures.Commands;
using MediatrExample.ApplicationCore.Features.RecetaFabricacionFeatures.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediatrExample.WebApi.Controllers;


[Authorize]
[ApiController]
[Route("api/recetafabricacionvw")]
public class RecetaFabricacionVWController : ControllerBase
{
    private readonly IMediator _mediator;

    public RecetaFabricacionVWController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Consulta recetas de fabricación
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<List<GetListRecetaFabricacionVWQueryResponse>> GetRecetaFabricacionVW() => 
        _mediator.Send(new GetListRecetaFabricacionVWQuery());

    /// <summary>
    /// Consulta una receta de fabricación por su ID
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpGet("{RecetaFabricacionId}")]
    public Task<GetRecetaFabricacionVWQueryResponse> GetRecetaFabricacionVWById([FromRoute] GetRecetaFabricacionVWQuery query) =>
        _mediator.Send(query);

    /// <summary>
    /// Crea una receta de fabricación nueva
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateRecetaFabricacion([FromBody] CreateRecetaFabricacionCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }

    /// <summary>
    /// revisa vigencias de las recetas de fabricación
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("checkreceta"), AllowAnonymous]
    public async Task<IActionResult> CheckRecetaFabricacion([FromBody] CheckRecetaFabricacionCommand command)
    {
        await _mediator.Send(command);

        return Ok();
    }
}
