using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dapper;
using MediatR;
using MediatrExample.ApplicationCore.Common.Exceptions;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Domain.View;
using MediatrExample.ApplicationCore.Features.PreparacionPastaFeatures.Queries;
using MediatrExample.ApplicationCore.Features.Products.Commands;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
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

        response.LstLineaProduccion = _context.LineasProduccion.Where(o => o.Estado == true).ToList();

        response.LstMateriaPrima = _context.MateriasPrimas.Where(o => o.Estado == true).ToList();

        var recetaLineaProduccion = _context.RecetasLineaProduccion.Where(o => o.RecetaFabricacionId == request.RecetaFabricacionId.FromHashId()).ToList();
        List<RecetaLineaProduccionExt2> recetaLineaProduccionExt = new();
        foreach (var itemLinea in recetaLineaProduccion)
        {
            var linea = _mapper.Map<RecetaLineaProduccionExt2>(itemLinea);
            linea.Variables = _context.RecetasMateriaPrima.Where(o => o.RecetaLineaProduccionId == linea.RecetaLineaProduccionId)
                .AsNoTracking()
                .ProjectTo<RecetaMateriaPrimaExt2>(_mapper.ConfigurationProvider)
                .ToList();
            recetaLineaProduccionExt.Add(linea);
        }
        response.RecetaLineaProduccion = recetaLineaProduccionExt;

        return response;
    }
}

public class GetRecetaFabricacionVWQueryResponse
{
    public string RecetaFabricacionId { get; set; }
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
    public List<LineaProduccion> LstLineaProduccion { get;set; }
    public List<MateriaPrima> LstMateriaPrima { get; set; }
    public List<RecetaLineaProduccionExt2> RecetaLineaProduccion { get; set; }
}

public class RecetaLineaProduccionExt2
{
    public int RecetaLineaProduccionId { get; set; }
    public int RecetaFabricacionId { get; set; }
    public int LineaProduccionId { get; set; }
    public string LineaProduccionNombre { get; set; } = default!;
    public List<RecetaMateriaPrimaExt2> Variables { get; set; }
}

public class RecetaMateriaPrimaExt2
{
    public int RecetaMateriaPrimaId { get; set; }
    public int RecetaLineaProduccionId { get; set; }
    public int MateriaPrimaId { get; set; }
    public string CodigoSap { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public decimal Valor { get; set; }
}

public class RecetaLineaProduccionExt2Mapper : Profile
{
    public RecetaLineaProduccionExt2Mapper() =>
        CreateMap<RecetaLineaProduccion, RecetaLineaProduccionExt2>();
}

public class RecetaMateriaPrimaExt2Mapper : Profile
{
    public RecetaMateriaPrimaExt2Mapper() =>
        CreateMap<RecetaMateriaPrima, RecetaMateriaPrimaExt2>();
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