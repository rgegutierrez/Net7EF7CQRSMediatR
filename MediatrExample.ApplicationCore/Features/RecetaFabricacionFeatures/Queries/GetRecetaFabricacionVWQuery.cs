using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MediatrExample.ApplicationCore.Common.Exceptions;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Domain.Receta;
using MediatrExample.ApplicationCore.Domain.View;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MediatrExample.ApplicationCore.Features.RecetaFabricacionFeatures.Queries;

public class GetRecetaFabricacionVWQuery : IRequest<GetRecetaFabricacionVWQueryResponse>
{
    public string RecetaFabricacionId { get; set; }
}

public class GetRecetaFabricacionVWQueryHandler : IRequestHandler<GetRecetaFabricacionVWQuery, GetRecetaFabricacionVWQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;
    readonly string _connectionString = "";

    public GetRecetaFabricacionVWQueryHandler(MyAppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<GetRecetaFabricacionVWQueryResponse> Handle(GetRecetaFabricacionVWQuery request, CancellationToken cancellationToken)
    {
        var obj = await _context.RecetasVW.FindAsync(request.RecetaFabricacionId.FromHashId());

        if (obj is null)
        {
            throw new NotFoundException(nameof(RecetaFabricacionVW), request.RecetaFabricacionId);
        }

        var response = _mapper.Map<GetRecetaFabricacionVWQueryResponse>(obj);

        // PARAMETROS
        response.LstTipoReceta = _context.TiposReceta.Where(o => o.Estado == true).ToList();
        response.LstLineaProduccion = _context.LineasProduccion.Where(o => o.Estado == true).ToList();
        response.LstMateriaPrima = _context.MateriasPrimas.Where(o => o.Estado == true).ToList();
        response.LstPreparacionPasta = _context.PreparacionPastas.Where(o => o.Estado == true).ToList();
        response.LstMaquinaPapelera = _context.MaquinasPapeleras.Where(o => o.Estado == true)
            .AsNoTracking()
            .ProjectTo<MaquinaPapeleraResponse>(_mapper.ConfigurationProvider)
            .ToList();

        foreach (var item in response.LstMaquinaPapelera)
        {
            item.Variables = _context.VariablesFormula.Where(o => o.MaquinaPapeleraId == item.MaquinaPapeleraId)
                .AsNoTracking()
                .ProjectTo<VariableFormulaResponse>(_mapper.ConfigurationProvider)
                .ToList();
        }

        // LINEA PRODUCCIÓN - MATERIA PRIMA
        var recetaLineaProduccion = _context.RecetasLineaProduccion.Where(
            o => o.RecetaFabricacionId == request.RecetaFabricacionId.FromHashId()
            ).ToList();
        List<RecetaLineaProduccionResponse> recetaLineaProduccionExt = new();
        foreach (var itemLinea in recetaLineaProduccion)
        {
            var linea = _mapper.Map<RecetaLineaProduccionResponse>(itemLinea);
            linea.Variables = _context.RecetasMateriaPrima.Where(o => o.RecetaLineaProduccionId == itemLinea.RecetaLineaProduccionId)
                .AsNoTracking()
                .ProjectTo<RecetaMateriaPrimaResponse>(_mapper.ConfigurationProvider)
                .ToList();
            recetaLineaProduccionExt.Add(linea);
        }
        response.RecetaLineaProduccion = recetaLineaProduccionExt;

        // LINEA PRODUCCIÓN - PREPARACION PASTA
        var recetaLineaPreparacion = _context.RecetasLineaPreparacion.Where(
            o => o.RecetaFabricacionId == request.RecetaFabricacionId.FromHashId()
            ).ToList();
        List<RecetaLineaPreparacionResponse> recetaLineaPreparacionExt = new();
        foreach (var itemLinea in recetaLineaPreparacion)
        {
            var linea = _mapper.Map<RecetaLineaPreparacionResponse>(itemLinea);
            linea.Parametros = _context.RecetasPreparacionPasta.Where(o => o.RecetaLineaPreparacionId == itemLinea.RecetaLineaPreparacionId)
                .AsNoTracking()
                .ProjectTo<RecetaPreparacionPastaResponse>(_mapper.ConfigurationProvider)
                .ToList();
            recetaLineaPreparacionExt.Add(linea);
        }
        response.RecetaLineaPreparacion = recetaLineaPreparacionExt;

        // LINEA PRODUCCIÓN - MAQUINA PAPELERA
        var recetaLineaMaquina = _context.RecetasLineaMaquina.Where(
            o => o.RecetaFabricacionId == request.RecetaFabricacionId.FromHashId()
            ).ToList();
        List<RecetaLineaMaquinaResponse> recetaLineaMaquinaExt = new();
        foreach (var itemLinea in recetaLineaMaquina)
        {
            var linea = _mapper.Map<RecetaLineaMaquinaResponse>(itemLinea);
            linea.Parametros = _context.RecetasMaquinaPapelera.Where(o => o.RecetaLineaMaquinaId == itemLinea.RecetaLineaMaquinaId)
                .AsNoTracking()
                .ProjectTo<RecetaMaquinaPapeleraResponse>(_mapper.ConfigurationProvider)
                .ToList();

            foreach (var item in linea.Parametros)
            {
                item.Variables = _context.RecetasVariableFormula.Where(o => o.RecetaMaquinaPapeleraId == item.RecetaMaquinaPapeleraId)
                    .AsNoTracking()
                    .ProjectTo<RecetaVariableFormulaResponse>(_mapper.ConfigurationProvider)
                    .ToList();
                item.RecetaLineaMaquinaId = 0;
            }
            recetaLineaMaquinaExt.Add(linea);
        }
        response.RecetaLineaMaquina = recetaLineaMaquinaExt;

        return response;
    }
}

public class GetRecetaFabricacionVWQueryResponse
{
    public string RecetaFabricacionId { get; set; } = default!;
    public int? TipoRecetaId { get; set; }
    public string? TipoRecetaNombre { get; set; }
    public string TipoPapelId { get; set; } = default!;
    public string TipoPapelCodigo { get; set; } = default!;
    public string TipoPapelNombre { get; set; } = default!;
    public string Gramaje { get; set; } = default!;
    public string ClienteId { get; set; } = default!;
    public string ClienteCodigo { get; set; } = default!;
    public string ClienteNombre { get; set; } = default!;
    public int TipoEspecificacionId { get; set; } = default!;
    public string TipoEspecificacionNombre { get; set; } = default!;
    public string TipoEspecificacionDsc { get; set; } = default!;
    public string Tubete { get; set; } = default!;
    public string Diametro { get; set; } = default!;
    public string Tolerancia { get; set; } = default!;
    public int EspecificacionTecnicaId { get; set; }
    public string CodigoReceta { get; set; } = default!;
    public int Version { get; set; }
    public int Estado { get; set; }
    public string? AprobacionGerencia { get; set; }
    public string? AprobacionJefatura { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedAtStr { get; set; }
    public DateTime? InicioVigencia { get; set; }
    public string? InicioVigenciaStr { get; set; }
    public DateTime? TerminoVigencia { get; set; }
    public string? TerminoVigenciaStr { get; set; }
    public List<TipoReceta> LstTipoReceta { get; set; }
    public List<LineaProduccion> LstLineaProduccion { get;set; }
    public List<MateriaPrima> LstMateriaPrima { get; set; }
    public List<PreparacionPasta> LstPreparacionPasta { get; set; }
    public List<MaquinaPapeleraResponse> LstMaquinaPapelera { get; set; }
    public List<RecetaLineaProduccionResponse> RecetaLineaProduccion { get; set; }
    public List<RecetaLineaMaquinaResponse> RecetaLineaMaquina { get; set; }
    public List<RecetaLineaPreparacionResponse> RecetaLineaPreparacion { get; set; }
}

public class RecetaLineaProduccionResponse : RecetaLineaProduccion
{
    public List<RecetaMateriaPrimaResponse> Variables { get; set; }
}

public class RecetaMateriaPrimaResponse : RecetaMateriaPrima
{

}

public class RecetaLineaPreparacionResponse : RecetaLineaPreparacion
{
    public List<RecetaPreparacionPastaResponse> Parametros { get; set; }
}

public class RecetaPreparacionPastaResponse : RecetaPreparacionPasta
{

}

public class RecetaLineaMaquinaResponse : RecetaLineaMaquina
{
    public List<RecetaMaquinaPapeleraResponse> Parametros { get; set; }
}

public class RecetaMaquinaPapeleraResponse : RecetaMaquinaPapelera 
{ 
    public List<RecetaVariableFormulaResponse> Variables { get; set; }
}

public class RecetaVariableFormulaResponse : RecetaVariableFormula
{

}

public class MaquinaPapeleraResponse : MaquinaPapelera
{
    public List<VariableFormulaResponse> Variables { get; set; }
}

public class VariableFormulaResponse : VariableFormula
{
    public string NombreVariable { get; set; } = default!;
}

public class GetRecetaFabricacionVWQueryProfile : Profile
{
    public GetRecetaFabricacionVWQueryProfile() =>
        CreateMap<RecetaFabricacionVW, GetRecetaFabricacionVWQueryResponse>()
            .ForMember(dest =>
                dest.RecetaFabricacionId,
                opt => opt.MapFrom(mf => mf.RecetaFabricacionId.ToHashId()))
            .ForMember(dest =>
                dest.CodigoReceta,
                opt => opt.MapFrom(mf => $"{mf.TipoPapelCodigo}.{mf.Gramaje}.{mf.ClienteCodigo}.{mf.Version.ToString("D3")}"))
            .ForMember(dest =>
                dest.CreatedAtStr,
                opt => opt.MapFrom(mf => AppHelpers.DatetimeToString(mf.CreatedAt, "dd/MM/yyyy HH:mm:ss")))
            .ForMember(dest =>
                dest.InicioVigenciaStr,
                opt => opt.MapFrom(mf => AppHelpers.DatetimeToString(mf.InicioVigencia, "dd/MM/yyyy HH:mm:ss")))
            .ForMember(dest =>
                dest.TerminoVigenciaStr,
                opt => opt.MapFrom(mf => AppHelpers.DatetimeToString(mf.TerminoVigencia, "dd/MM/yyyy HH:mm:ss")));
}

public class RecetaLineaProduccionResponseMapper : Profile
{
    public RecetaLineaProduccionResponseMapper() =>
        CreateMap<RecetaLineaProduccion, RecetaLineaProduccionResponse>()
            .ForMember(dest => dest.RecetaLineaProduccionId, act => act.Ignore())
            .ForMember(dest => dest.RecetaFabricacion, act => act.Ignore())
            .ForMember(dest => dest.LineaProduccion, act => act.Ignore());
}

public class RecetaMateriaPrimaResponseMapper : Profile
{
    public RecetaMateriaPrimaResponseMapper() =>
        CreateMap<RecetaMateriaPrima, RecetaMateriaPrimaResponse>()
            .ForMember(dest => dest.RecetaMateriaPrimaId, act => act.Ignore())
            .ForMember(dest => dest.MateriaPrima, act => act.Ignore())
            .ForMember(dest => dest.RecetaLineaProduccion, act => act.Ignore());
}

public class RecetaLineaPreparacionResponseMapper : Profile
{
    public RecetaLineaPreparacionResponseMapper() =>
        CreateMap<RecetaLineaPreparacion, RecetaLineaPreparacionResponse>()
            .ForMember(dest => dest.RecetaLineaPreparacionId, act => act.Ignore())
            .ForMember(dest => dest.RecetaFabricacion, act => act.Ignore())
            .ForMember(dest => dest.LineaProduccion, act => act.Ignore());
}

public class RecetaPreparacionPastaResponseMapper : Profile
{
    public RecetaPreparacionPastaResponseMapper() =>
        CreateMap<RecetaPreparacionPasta, RecetaPreparacionPastaResponse>()
            .ForMember(dest => dest.RecetaPreparacionPastaId, act => act.Ignore())
            .ForMember(dest => dest.PreparacionPasta, act => act.Ignore())
            .ForMember(dest => dest.RecetaLineaPreparacion, act => act.Ignore());
}

public class RecetaLineaMaquinaResponseMapper : Profile
{
    public RecetaLineaMaquinaResponseMapper() =>
        CreateMap<RecetaLineaMaquina, RecetaLineaMaquinaResponse>()
            .ForMember(dest => dest.RecetaLineaMaquinaId, act => act.Ignore())
            .ForMember(dest => dest.RecetaFabricacion, act => act.Ignore())
            .ForMember(dest => dest.LineaProduccion, act => act.Ignore());
}

public class RecetaMaquinaPapeleraResponseMapper : Profile
{
    public RecetaMaquinaPapeleraResponseMapper() =>
        CreateMap<RecetaMaquinaPapelera, RecetaMaquinaPapeleraResponse>()
            .ForMember(dest => dest.RecetaLineaMaquina, act => act.Ignore())
            .ForMember(dest => dest.MaquinaPapelera, act => act.Ignore());
}

public class RecetaVariableFormulaResponseMapper : Profile
{
    public RecetaVariableFormulaResponseMapper() =>
        CreateMap<RecetaVariableFormula, RecetaVariableFormulaResponse>()
            .ForMember(dest => dest.RecetaVariableFormulaId, act => act.Ignore())
            .ForMember(dest => dest.RecetaMaquinaPapelera, act => act.Ignore());
}

public class MaquinaPapeleraResponseMapper : Profile
{
    public MaquinaPapeleraResponseMapper() =>
        CreateMap<MaquinaPapelera, MaquinaPapeleraResponse>();
}

public class VariableFormulaResponseMapper : Profile
{
    public VariableFormulaResponseMapper() =>
        CreateMap<VariableFormula, VariableFormulaResponse>()
            .ForMember(dest => dest.NombreVariable, act => act.MapFrom(mf => mf.Variable.NombreVariable))
            .ForMember(dest => dest.VariableFormulaId, act => act.Ignore())
            .ForMember(dest => dest.MaquinaPapelera, act => act.Ignore());
}