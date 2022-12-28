using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.Products.Commands;
public class CreateRecetaFabricacionCommand : IRequest
{
    public string RecetaFabricacionId { get; set; }
    public List<RecetaLineaProduccionExt> RecetaLineaProduccion { get; set; } 
}

public class RecetaLineaProduccionExt : RecetaLineaProduccion
{
    public List<RecetaMateriaPrima> Variables { get; set; }
}

public class CreateRecetaFabricacionCommandHandler : IRequestHandler<CreateRecetaFabricacionCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateRecetaFabricacionCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateRecetaFabricacionCommand request, CancellationToken cancellationToken)
    {
        foreach (var itemLinea in _context.RecetasLineaProduccion.Where(o => o.RecetaFabricacionId == request.RecetaFabricacionId.FromHashId()).ToList())
        {
            _context.RecetasLineaProduccion.Remove(itemLinea);
        }
        await _context.SaveChangesAsync();

        foreach (var itemLinea in request.RecetaLineaProduccion)
        {
            itemLinea.RecetaLineaProduccionId = 0;
            itemLinea.RecetaFabricacionId = request.RecetaFabricacionId.FromHashId();
            _context.RecetasLineaProduccion.Add(itemLinea);
            await _context.SaveChangesAsync();

            foreach (var itemMateriaPrima in itemLinea.Variables)
            {
                itemMateriaPrima.RecetaMateriaPrimaId = 0;
                itemMateriaPrima.RecetaLineaProduccionId = itemLinea.RecetaLineaProduccionId;
                _context.RecetasMateriaPrima.Add(itemMateriaPrima);
                await _context.SaveChangesAsync();
            }
        }

        return Unit.Value;
    }
}
public class CreateRecetaFabricacionCommandMapper : Profile
{
    public CreateRecetaFabricacionCommandMapper() =>
        CreateMap<CreateRecetaFabricacionCommand, RecetaFabricacion>();
}

public class CreateRecetaFabricacionValidator : AbstractValidator<CreateRecetaFabricacionCommand>
{
    public CreateRecetaFabricacionValidator()
    {

    }
}
