using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MediatrExample.ApplicationCore.Common.Exceptions;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Domain.Receta;
using MediatrExample.ApplicationCore.Domain.View;
using MediatrExample.ApplicationCore.Features.RecetaFabricacionFeatures.Commands;
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
        response.LstTipoReceta = _context.TiposReceta.Where(o => o.Estado == true).OrderBy(o => o.NombreVariable).ToList();
        response.LstLineaProduccion = _context.LineasProduccion.Where(o => o.Estado == true).ToList();
        response.LstMateriaPrima = _context.MateriasPrimas.Where(o => o.Estado == true)
            .AsNoTracking()
            .ProjectTo<MateriaPrimaResponse>(_mapper.ConfigurationProvider)
            .OrderBy(o => o.NombreVariable).ToList();
        response.LstPreparacionPasta = _context.PreparacionPastas.Where(o => o.Estado == true)
            .AsNoTracking()
            .ProjectTo<PreparacionPastaResponse>(_mapper.ConfigurationProvider)
            .OrderBy(o => o.NombreVariable).ToList();
        response.LstMaquinaPapelera = _context.MaquinasPapeleras.Where(o => o.Estado == true)
            .OrderBy(o => o.Orden)
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
        response.LstProductoQuimico = _context.ProductosQuimicos.Where(o => o.Estado == true)
            .AsNoTracking()
            .ProjectTo<ProductoQuimicoResponse>(_mapper.ConfigurationProvider)
            .OrderBy(o => o.NombreVariable).ToList();
        response.LstTiroMaquina = _context.TirosMaquina.Where(o => o.Estado == true)
            .AsNoTracking()
            .ProjectTo<TiroMaquinaResponse>(_mapper.ConfigurationProvider)
            .OrderBy(o => o.NombreVariable).ToList();
        response.LstFormacion = _context.Formaciones.Where(o => o.Estado == true)
            .AsNoTracking()
            .ProjectTo<FormacionResponse>(_mapper.ConfigurationProvider)
            .OrderBy(o => o.NombreVariable).ToList();
        response.LstTipoIndicadorVacio = _context.TipoIndicadoresVacio
            .AsNoTracking()
            .ProjectTo<TipoIndicadorVacioResponse>(_mapper.ConfigurationProvider)
            .OrderBy(o => o.NombreVariable).ToList();

        foreach (var item in response.LstTipoIndicadorVacio)
        {
            item.LstIndicadorVacio = _context.IndicadoresVacio
                .Where(o => o.Estado == true && o.TipoIndicadorVacioId == item.TipoIndicadorVacioId)
                .AsNoTracking()
                .ProjectTo<IndicadorVacioResponse>(_mapper.ConfigurationProvider)
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

        // PRODUCTO QUIMICO
        response.RecetaProductoQuimico = _context.RecetasProductoQuimico.Where(
            o => o.RecetaFabricacionId == request.RecetaFabricacionId.FromHashId()
            ).AsNoTracking()
            .ProjectTo<RecetaProductoQuimicoResponse>(_mapper.ConfigurationProvider)
            .ToList();

        // TIRO MAQUINA
        response.RecetaTiroMaquina = _context.RecetasTiroMaquina.Where(
            o => o.RecetaFabricacionId == request.RecetaFabricacionId.FromHashId()
            ).AsNoTracking()
            .ProjectTo<RecetaTiroMaquinaResponse>(_mapper.ConfigurationProvider)
            .ToList();

        // FORMACION
        var recetaFormacion = _context.RecetasFormacion.Where(
            o => o.RecetaFabricacionId == request.RecetaFabricacionId.FromHashId()
            ).AsNoTracking()
            .ToList();
        List<RecetaFormacionResponse> recetaFormacionExt = new();
        foreach (var item in recetaFormacion)
        {
            var formacion = _mapper.Map<RecetaFormacionResponse>(item);
            formacion.Valores = _context.RecetasFormacionValor
                .Where(o => o.RecetaFormacionId == item.RecetaFormacionId)
                .OrderBy(o => o.Foil)
                .ProjectTo<RecetaFormacionValorResponse>(_mapper.ConfigurationProvider)
                .ToList();
            recetaFormacionExt.Add(formacion);
        }
        response.RecetaFormacion = recetaFormacionExt;

        // INDICADOR VACIO
        var recetaTipoIndicadorVacio = _context.RecetasTipoIndicadorVacio.Where(
            o => o.RecetaFabricacionId == request.RecetaFabricacionId.FromHashId()
            ).ToList();
        List<RecetaTipoIndicadorVacioResponse> recetaTipoIndicadorVacioExt = new();
        foreach (var itemTipoIndicadorVacio in recetaTipoIndicadorVacio)
        {
            var lstTipoIndicador = _mapper.Map<RecetaTipoIndicadorVacioResponse>(itemTipoIndicadorVacio);
            lstTipoIndicador.RecetaIndicadorVacio = _context.RecetasIndicadorVacio
                .Where(o => o.RecetaTipoIndicadorVacioId == itemTipoIndicadorVacio.RecetaTipoIndicadorVacioId)
                .AsNoTracking()
                .ProjectTo<RecetaIndicadorVacioResponse>(_mapper.ConfigurationProvider)
                .ToList();

            recetaTipoIndicadorVacioExt.Add(lstTipoIndicador);
        }
        response.RecetaTipoIndicadorVacio = recetaTipoIndicadorVacioExt;

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
    public List<MateriaPrimaResponse> LstMateriaPrima { get; set; }
    public List<PreparacionPastaResponse> LstPreparacionPasta { get; set; }
    public List<MaquinaPapeleraResponse> LstMaquinaPapelera { get; set; }
    public List<ProductoQuimicoResponse> LstProductoQuimico { get; set; }
    public List<TiroMaquinaResponse> LstTiroMaquina { get; set; }
    public List<FormacionResponse> LstFormacion { get; set; }
    public List<TipoIndicadorVacioResponse> LstTipoIndicadorVacio { get; set; }
    public List<RecetaLineaProduccionResponse> RecetaLineaProduccion { get; set; }
    public List<RecetaLineaMaquinaResponse> RecetaLineaMaquina { get; set; }
    public List<RecetaLineaPreparacionResponse> RecetaLineaPreparacion { get; set; }
    public List<RecetaProductoQuimicoResponse> RecetaProductoQuimico { get; set; }
    public List<RecetaTiroMaquinaResponse> RecetaTiroMaquina { get; set; }
    public List<RecetaFormacionResponse> RecetaFormacion { get; set; }
    public List<RecetaTipoIndicadorVacioResponse> RecetaTipoIndicadorVacio { get; set; }
}

public class MateriaPrimaResponse : MateriaPrima {
    public string ValorMinimoStr { get; set; } = default!;
    public string ValorMaximoStr { get; set; } = default!;
}

public class PreparacionPastaResponse : PreparacionPasta {
    public string ValorMinimoStr { get; set; } = default!;
    public string ValorMaximoStr { get; set; } = default!;
}

public class ProductoQuimicoResponse : ProductoQuimico {
    public string ValorMinimoStr { get; set; } = default!;
    public string ValorMaximoStr { get; set; } = default!;
}

public class TiroMaquinaResponse : TiroMaquina {
    public string ValorMinimoStr { get; set; } = default!;
    public string ValorMaximoStr { get; set; } = default!;
}

public class FormacionResponse : Formacion
{
    public string AnguloMinimoStr { get; set; } = default!;
    public string AnguloMaximoStr { get; set; } = default!;
    public string AlturaMinimoStr { get; set; } = default!;
    public string AlturaMaximoStr { get; set; } = default!;
}

public class TipoIndicadorVacioResponse : TipoIndicadorVacio
{
    public List<IndicadorVacioResponse> LstIndicadorVacio { get; set; }
}

public class IndicadorVacioResponse : IndicadorVacio
{
    public string ValorMinimoStr { get; set; } = default!;
    public string ValorMaximoStr { get; set; } = default!;
}

public class RecetaLineaProduccionResponse : RecetaLineaProduccion
{
    public List<RecetaMateriaPrimaResponse> Variables { get; set; }
}

public class RecetaMateriaPrimaResponse : RecetaMateriaPrima
{
    public string ValorMinimoStr { get; set; } = default!;
    public string ValorMaximoStr { get; set; } = default!;
}

public class RecetaLineaPreparacionResponse : RecetaLineaPreparacion
{
    public List<RecetaPreparacionPastaResponse> Parametros { get; set; }
}

public class RecetaPreparacionPastaResponse : RecetaPreparacionPasta
{
    public string ValorMinimoStr { get; set; } = default!;
    public string ValorMaximoStr { get; set; } = default!;
}

public class RecetaLineaMaquinaResponse : RecetaLineaMaquina
{
    public List<RecetaMaquinaPapeleraResponse> Parametros { get; set; }
}

public class RecetaMaquinaPapeleraResponse : RecetaMaquinaPapelera 
{
    public string ValorMinimoStr { get; set; } = default!;
    public string ValorMaximoStr { get; set; } = default!;
    public List<RecetaVariableFormulaResponse> Variables { get; set; }
}

public class RecetaVariableFormulaResponse : RecetaVariableFormula
{

}

public class MaquinaPapeleraResponse : MaquinaPapelera
{
    public string ValorMinimoStr { get; set; } = default!;
    public string ValorMaximoStr { get; set; } = default!;
    public List<VariableFormulaResponse> Variables { get; set; }
}

public class VariableFormulaResponse : VariableFormula
{
    public string NombreVariable { get; set; } = default!;
}

public class RecetaProductoQuimicoResponse : RecetaProductoQuimico
{
    public string ValorMinimoStr { get; set; } = default!;
    public string ValorMaximoStr { get; set; } = default!;
}

public class RecetaTiroMaquinaResponse : RecetaTiroMaquina
{
    public string ValorMinimoStr { get; set; } = default!;
    public string ValorMaximoStr { get; set; } = default!;
}

public class RecetaFormacionResponse : RecetaFormacion
{
    public string AnguloMinimoStr { get; set; } = default!;
    public string AnguloMaximoStr { get; set; } = default!;
    public string AlturaMinimoStr { get; set; } = default!;
    public string AlturaMaximoStr { get; set; } = default!;
    public List<RecetaFormacionValorResponse> Valores { get; set; }
}

public class RecetaFormacionValorResponse : RecetaFormacionValor
{

}

public class RecetaTipoIndicadorVacioResponse : RecetaTipoIndicadorVacio
{
    public List<RecetaIndicadorVacioResponse> RecetaIndicadorVacio { get; set; }
}

public class RecetaIndicadorVacioResponse : RecetaIndicadorVacio
{
    public string ValorMinimoStr { get; set; } = default!;
    public string ValorMaximoStr { get; set; } = default!;
}

public class MateriaPrimaResponseMapper : Profile
{
    public MateriaPrimaResponseMapper() =>
        CreateMap<MateriaPrima, MateriaPrimaResponse>()
            .ForMember(dest =>
                dest.ValorMinimoStr,
                opt => opt.MapFrom(mf => mf.ValorMinimo.FromDotToComma()))
            .ForMember(dest =>
                dest.ValorMaximoStr,
                opt => opt.MapFrom(mf => mf.ValorMaximo.FromDotToComma()));
}

public class PreparacionPastaResponseMapper : Profile
{
    public PreparacionPastaResponseMapper() =>
        CreateMap<PreparacionPasta, PreparacionPastaResponse>()
            .ForMember(dest =>
                dest.ValorMinimoStr,
                opt => opt.MapFrom(mf => mf.ValorMinimo.FromDotToComma()))
            .ForMember(dest =>
                dest.ValorMaximoStr,
                opt => opt.MapFrom(mf => mf.ValorMaximo.FromDotToComma()));
}

public class ProductoQuimicoResponseMapper : Profile
{
    public ProductoQuimicoResponseMapper() =>
        CreateMap<ProductoQuimico, ProductoQuimicoResponse>()
            .ForMember(dest =>
                dest.ValorMinimoStr,
                opt => opt.MapFrom(mf => mf.ValorMinimo.FromDotToComma()))
            .ForMember(dest =>
                dest.ValorMaximoStr,
                opt => opt.MapFrom(mf => mf.ValorMaximo.FromDotToComma()));
}

public class TiroMaquinaResponseMapper : Profile
{
    public TiroMaquinaResponseMapper() =>
        CreateMap<TiroMaquina, TiroMaquinaResponse>()
            .ForMember(dest =>
                dest.ValorMinimoStr,
                opt => opt.MapFrom(mf => mf.ValorMinimo.FromDotToComma()))
            .ForMember(dest =>
                dest.ValorMaximoStr,
                opt => opt.MapFrom(mf => mf.ValorMaximo.FromDotToComma()));
}

public class FormacionResponseMapper : Profile
{
    public FormacionResponseMapper() =>
        CreateMap<Formacion, FormacionResponse>()
            .ForMember(dest =>
                dest.AnguloMinimoStr,
                opt => opt.MapFrom(mf => mf.RangoAnguloMinimo.FromDotToComma()))
            .ForMember(dest =>
                dest.AnguloMaximoStr,
                opt => opt.MapFrom(mf => mf.RangoAnguloMaximo.FromDotToComma()))
            .ForMember(dest =>
                dest.AlturaMinimoStr,
                opt => opt.MapFrom(mf => mf.RangoAlturaMinimo.FromDotToComma()))
            .ForMember(dest =>
                dest.AlturaMaximoStr,
                opt => opt.MapFrom(mf => mf.RangoAlturaMaximo.FromDotToComma()));
}

public class TipoIndicadorVacioResponseMapper : Profile
{
    public TipoIndicadorVacioResponseMapper() =>
        CreateMap<TipoIndicadorVacio, TipoIndicadorVacioResponse>();
}

public class IndicadorVacioResponseMapper : Profile
{
    public IndicadorVacioResponseMapper() =>
        CreateMap<IndicadorVacio, IndicadorVacioResponse>()
            .ForMember(dest =>
                dest.ValorMinimoStr,
                opt => opt.MapFrom(mf => mf.ValorMinimo.FromDotToComma()))
            .ForMember(dest =>
                dest.ValorMaximoStr,
                opt => opt.MapFrom(mf => mf.ValorMaximo.FromDotToComma()));
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
            .ForMember(dest =>
                dest.ValorMinimoStr,
                opt => opt.MapFrom(mf => mf.ValorMinimo.FromDotToComma()))
            .ForMember(dest =>
                dest.ValorMaximoStr,
                opt => opt.MapFrom(mf => mf.ValorMaximo.FromDotToComma()))
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
            .ForMember(dest =>
                dest.ValorMinimoStr,
                opt => opt.MapFrom(mf => mf.ValorMinimo.FromDotToComma()))
            .ForMember(dest =>
                dest.ValorMaximoStr,
                opt => opt.MapFrom(mf => mf.ValorMaximo.FromDotToComma()))
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
            .ForMember(dest =>
                dest.ValorMinimoStr,
                opt => opt.MapFrom(mf => mf.ValorMinimo.FromDotToComma()))
            .ForMember(dest =>
                dest.ValorMaximoStr,
                opt => opt.MapFrom(mf => mf.ValorMaximo.FromDotToComma()))
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
        CreateMap<MaquinaPapelera, MaquinaPapeleraResponse>()
            .ForMember(dest =>
                dest.ValorMinimoStr,
                opt => opt.MapFrom(mf => mf.ValorMinimo.FromDotToComma()))
            .ForMember(dest =>
                dest.ValorMaximoStr,
                opt => opt.MapFrom(mf => mf.ValorMaximo.FromDotToComma()));
}

public class VariableFormulaResponseMapper : Profile
{
    public VariableFormulaResponseMapper() =>
        CreateMap<VariableFormula, VariableFormulaResponse>()
            .ForMember(dest => dest.NombreVariable, act => act.MapFrom(mf => mf.Variable.NombreVariable))
            .ForMember(dest => dest.VariableFormulaId, act => act.Ignore())
            .ForMember(dest => dest.MaquinaPapelera, act => act.Ignore());
}

public class RecetaProductoQuimicoResponseMapper : Profile
{
    public RecetaProductoQuimicoResponseMapper() =>
        CreateMap<RecetaProductoQuimico, RecetaProductoQuimicoResponse>()
            .ForMember(dest =>
                dest.ValorMinimoStr,
                opt => opt.MapFrom(mf => mf.ValorMinimo.FromDotToComma()))
            .ForMember(dest =>
                dest.ValorMaximoStr,
                opt => opt.MapFrom(mf => mf.ValorMaximo.FromDotToComma()))
            .ForMember(dest => dest.RecetaProductoQuimicoId, act => act.Ignore())
            .ForMember(dest => dest.RecetaFabricacion, act => act.Ignore());
}

public class RecetaTiroMaquinaResponseMapper : Profile
{
    public RecetaTiroMaquinaResponseMapper() =>
        CreateMap<RecetaTiroMaquina, RecetaTiroMaquinaResponse>()
            .ForMember(dest =>
                dest.ValorMinimoStr,
                opt => opt.MapFrom(mf => mf.ValorMinimo.FromDotToComma()))
            .ForMember(dest =>
                dest.ValorMaximoStr,
                opt => opt.MapFrom(mf => mf.ValorMaximo.FromDotToComma()))
            .ForMember(dest => dest.RecetaTiroMaquinaId, act => act.Ignore())
            .ForMember(dest => dest.RecetaFabricacion, act => act.Ignore());
}

public class RecetaFormacionResponseMapper : Profile
{
    public RecetaFormacionResponseMapper() =>
        CreateMap<RecetaFormacion, RecetaFormacionResponse>()
            .ForMember(dest =>
                dest.AnguloMinimoStr,
                opt => opt.MapFrom(mf => mf.RangoAnguloMinimo.FromDotToComma()))
            .ForMember(dest =>
                dest.AnguloMaximoStr,
                opt => opt.MapFrom(mf => mf.RangoAnguloMaximo.FromDotToComma()))
            .ForMember(dest =>
                dest.AlturaMinimoStr,
                opt => opt.MapFrom(mf => mf.RangoAlturaMinimo.FromDotToComma()))
            .ForMember(dest =>
                dest.AlturaMaximoStr,
                opt => opt.MapFrom(mf => mf.RangoAlturaMaximo.FromDotToComma()))
            .ForMember(dest => dest.RecetaFormacionId, act => act.Ignore())
            .ForMember(dest => dest.RecetaFabricacion, act => act.Ignore());
}

public class RecetaFormacionValorResponseMapper : Profile
{
    public RecetaFormacionValorResponseMapper() =>
        CreateMap<RecetaFormacionValor, RecetaFormacionValorResponse>()
            .ForMember(dest => dest.RecetaFormacionValorId, act => act.Ignore())
            .ForMember(dest => dest.RecetaFormacion, act => act.Ignore());
}

public class RecetaTipoIndicadorVacioResponseMapper : Profile
{
    public RecetaTipoIndicadorVacioResponseMapper() =>
        CreateMap<RecetaTipoIndicadorVacio, RecetaTipoIndicadorVacioResponse>()
            .ForMember(dest => dest.RecetaTipoIndicadorVacioId, act => act.Ignore())
            .ForMember(dest => dest.RecetaFabricacion, act => act.Ignore())
            .ForMember(dest => dest.TipoIndicadorVacio, act => act.Ignore());
}

public class RecetaIndicadorVacioResponseMapper : Profile
{
    public RecetaIndicadorVacioResponseMapper() =>
        CreateMap<RecetaIndicadorVacio, RecetaIndicadorVacioResponse>()
            .ForMember(dest =>
                dest.ValorMinimoStr,
                opt => opt.MapFrom(mf => mf.ValorMinimo.FromDotToComma()))
            .ForMember(dest =>
                dest.ValorMaximoStr,
                opt => opt.MapFrom(mf => mf.ValorMaximo.FromDotToComma()))
            .ForMember(dest => dest.RecetaIndicadorVacioId, act => act.Ignore())
            .ForMember(dest => dest.IndicadorVacio, act => act.Ignore())
            .ForMember(dest => dest.RecetaTipoIndicadorVacio, act => act.Ignore());
}