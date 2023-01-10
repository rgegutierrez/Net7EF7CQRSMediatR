using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Domain.Receta;
using MediatrExample.ApplicationCore.Features.RecetaFabricacionFeatures.Queries;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations;
using Microsoft.EntityFrameworkCore;

namespace MediatrExample.ApplicationCore.Features.RecetaFabricacionFeatures.Commands;
public class CreateRecetaFabricacionCommand : IRequest
{
    public string RecetaFabricacionId { get; set; }
    public List<RecetaLineaProduccionRequest> RecetaLineaProduccion { get; set; }
    public List<RecetaLineaMaquinaRequest> RecetaLineaMaquina { get; set; }
}

public class RecetaLineaProduccionRequest : RecetaLineaProduccion
{
    public List<RecetaMateriaPrimaRequest> Variables { get; set; }
}

public class RecetaMateriaPrimaRequest : RecetaMateriaPrima
{

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
        await _context.SaveChangesAsync();

        var aux = _context.RecetasLineaMaquina.Where(
                    o => o.RecetaFabricacionId == request.RecetaFabricacionId.FromHashId()
                    ).ToList();

        foreach (var item in aux)
        {
            var aux2 = _context.RecetasMaquinaPapelera.Where(
                    o => o.RecetaLineaMaquinaId == item.RecetaLineaMaquinaId
                    ).ToList();
            foreach (var i in aux2)
            {
                var aux3 = _context.RecetasVariableFormula.Where(
                    o => o.RecetaMaquinaPapeleraId == i.RecetaMaquinaPapeleraId
                    ).ToList();
                _context.RecetasVariableFormula.RemoveRange(
                    aux3
                    );
                _context.RecetasMaquinaPapelera.Remove(i);
                await _context.SaveChangesAsync();
            }
        }

        _context.RecetasLineaMaquina.RemoveRange(
            aux
            );
        await _context.SaveChangesAsync();

        // LINEA PRODUCCIÓN - MAQUINA PAPELERA
        foreach (var itemLinea in request.RecetaLineaMaquina)
        {
            var rLineaMaquina = _mapper.Map<RecetaLineaMaquina>(itemLinea);
            rLineaMaquina.RecetaFabricacionId = request.RecetaFabricacionId.FromHashId();
            _context.RecetasLineaMaquina.Add(rLineaMaquina);
            await _context.SaveChangesAsync();

            foreach (var itemMaquina in itemLinea.Parametros)
            {
                var rMaquinaPapelera = _mapper.Map<RecetaMaquinaPapelera>(itemMaquina);
                rMaquinaPapelera.RecetaLineaMaquinaId = rLineaMaquina.RecetaLineaMaquinaId;
                _context.RecetasMaquinaPapelera.Add(rMaquinaPapelera);
                await _context.SaveChangesAsync();

                if (itemMaquina.ModoIngreso)
                {
                    foreach (var itemVariableFormula in itemMaquina.Variables)
                    {
                        var rVariableFormula = _mapper.Map<RecetaVariableFormula>(itemVariableFormula);
                        rVariableFormula.RecetaMaquinaPapeleraId = rMaquinaPapelera.RecetaMaquinaPapeleraId;
                        _context.RecetasVariableFormula.Add(rVariableFormula);
                        await _context.SaveChangesAsync();
                    }
                }

            }
        }

        // LINEA PRODUCCIÓN - MATERIA PRIMA
        foreach (var itemLinea in request.RecetaLineaProduccion)
        {
            var rLineaProduccion = _mapper.Map<RecetaLineaProduccion>(itemLinea);
            rLineaProduccion.RecetaFabricacionId = request.RecetaFabricacionId.FromHashId();
            _context.RecetasLineaProduccion.Add(rLineaProduccion);
            await _context.SaveChangesAsync();

            foreach (var itemMateriaPrima in itemLinea.Variables)
            {
                var rMateriaPrima = _mapper.Map<RecetaMateriaPrima>(itemMateriaPrima);
                rMateriaPrima.RecetaLineaProduccionId = rLineaProduccion.RecetaLineaProduccionId;
                _context.RecetasMateriaPrima.Add(rMateriaPrima);
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

public class RecetaLineaProduccionRequestMapper : Profile
{
    public RecetaLineaProduccionRequestMapper() =>
        CreateMap<RecetaLineaProduccionRequest, RecetaLineaProduccion>()
            .ForMember(dest => dest.RecetaLineaProduccionId, act => act.Ignore())
            .ForMember(dest => dest.RecetaFabricacion, act => act.Ignore())
            .ForMember(dest => dest.LineaProduccion, act => act.Ignore());
}

public class RecetaMateriaPrimaRequestMapper : Profile
{
    public RecetaMateriaPrimaRequestMapper() =>
        CreateMap<RecetaMateriaPrimaRequest, RecetaMateriaPrima>()
            .ForMember(dest => dest.RecetaMateriaPrimaId, act => act.Ignore())
            .ForMember(dest => dest.MateriaPrima, act => act.Ignore())
            .ForMember(dest => dest.RecetaLineaProduccion, act => act.Ignore());
}

public class RecetaLineaMaquinaRequestMapper : Profile
{
    public RecetaLineaMaquinaRequestMapper() =>
        CreateMap<RecetaLineaMaquinaRequest, RecetaLineaMaquina>()
            .ForMember(dest => dest.RecetaLineaMaquinaId, act => act.Ignore())
            .ForMember(dest => dest.RecetaFabricacion, act => act.Ignore())
            .ForMember(dest => dest.LineaProduccion, act => act.Ignore());
}

public class RecetaMaquinaPapeleraRequestMapper : Profile
{
    public RecetaMaquinaPapeleraRequestMapper() =>
        CreateMap<RecetaMaquinaPapeleraRequest, RecetaMaquinaPapelera>()
            .ForMember(dest => dest.RecetaMaquinaPapeleraId, act => act.Ignore())
            .ForMember(dest => dest.RecetaLineaMaquina, act => act.Ignore())
            .ForMember(dest => dest.MaquinaPapelera, act => act.Ignore());
}

public class RecetaVariableFormulaRequestMapper : Profile
{
    public RecetaVariableFormulaRequestMapper() =>
        CreateMap<RecetaVariableFormulaRequest, RecetaVariableFormula>()
            .ForMember(dest => dest.RecetaVariableFormulaId, act => act.Ignore())
            .ForMember(dest => dest.RecetaMaquinaPapelera, act => act.Ignore());
}

public class CreateRecetaFabricacionValidator : AbstractValidator<CreateRecetaFabricacionCommand>
{
    public CreateRecetaFabricacionValidator()
    {

    }
}
