using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Domain.Receta;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.Products.Commands;
public class CreateRecetaFabricacionCommand : IRequest
{
    public string RecetaFabricacionId { get; set; }
    public List<RecetaLineaProduccionRequest> RecetaLineaProduccion { get; set; }
    public List<RecetaLineaMaquinaRequest> RecetaLineaMaquina { get; set; }
}

public class RecetaLineaProduccionRequest : RecetaLineaProduccion
{
    public List<RecetaMateriaPrima> Variables { get; set; }
}
public class RecetaLineaMaquinaRequest : RecetaLineaMaquina
{
    public List<RecetaMaquinaPapeleraRequest> Parametros { get; set; }
}

public class RecetaMaquinaPapeleraRequest : RecetaMaquinaPapelera
{
    public List<RecetaVariableFormulaRequest> Variables { get; set; }
}

public class RecetaVariableFormulaRequest : RecetaVariableFormula
{

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
        _context.RecetasLineaProduccion.RemoveRange(
            _context.RecetasLineaProduccion.Where(
                o => o.RecetaFabricacionId == request.RecetaFabricacionId.FromHashId()
                )
            );
        _context.RecetasLineaMaquina.RemoveRange(
            _context.RecetasLineaMaquina.Where(
                o => o.RecetaFabricacionId == request.RecetaFabricacionId.FromHashId()
                )
            );
        await _context.SaveChangesAsync();

        // LINEA PRODUCCIÓN - MAQUINA PAPELERA
        foreach (var itemLinea in request.RecetaLineaMaquina)
        {
            itemLinea.RecetaFabricacionId = request.RecetaFabricacionId.FromHashId();
            _context.RecetasLineaMaquina.Add(itemLinea);
            await _context.SaveChangesAsync();

            foreach (var itemMaquina in itemLinea.Parametros)
            {
                itemMaquina.RecetaLineaMaquinaId = itemLinea.RecetaLineaMaquinaId;
                _context.RecetasMaquinaPapelera.Add(itemMaquina);
                await _context.SaveChangesAsync();

                if (itemMaquina.ModoIngreso)
                {
                    foreach (var itemVariableFormula in itemMaquina.Variables)
                    {
                        itemVariableFormula.RecetaMaquinaPapeleraId = itemMaquina.RecetaMaquinaPapeleraId;
                        _context.RecetasVariableFormula.Add(itemVariableFormula);
                        await _context.SaveChangesAsync();
                    }
                }

            }
        }

        // LINEA PRODUCCIÓN - MATERIA PRIMA
        foreach (var itemLinea in request.RecetaLineaProduccion)
        {
            itemLinea.RecetaFabricacionId = request.RecetaFabricacionId.FromHashId();
            _context.RecetasLineaProduccion.Add(itemLinea);
            await _context.SaveChangesAsync();

            foreach (var itemMateriaPrima in itemLinea.Variables)
            {
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
