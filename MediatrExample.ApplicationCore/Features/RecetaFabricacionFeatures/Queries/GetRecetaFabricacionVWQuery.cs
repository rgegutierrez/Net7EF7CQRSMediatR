using AutoMapper;
using Dapper;
using MediatR;
using MediatrExample.ApplicationCore.Common.Exceptions;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain.View;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
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