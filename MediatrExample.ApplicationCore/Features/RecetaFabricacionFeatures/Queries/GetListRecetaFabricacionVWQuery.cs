using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain.View;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MediatrExample.ApplicationCore.Features.RecetaFabricacionVWFeatures.Queries;

public class GetListRecetaFabricacionVWQuery : IRequest<List<GetListRecetaFabricacionVWQueryResponse>>
{

}

public class GetListRecetaFabricacionVWQueryHandler : IRequestHandler<GetListRecetaFabricacionVWQuery, List<GetListRecetaFabricacionVWQueryResponse>>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetListRecetaFabricacionVWQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<GetListRecetaFabricacionVWQueryResponse>> Handle(GetListRecetaFabricacionVWQuery request, CancellationToken cancellationToken)
    {
        return _context.RecetasVW
            .AsNoTracking()
            .ProjectTo<GetListRecetaFabricacionVWQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}

public class GetListRecetaFabricacionVWQueryResponse
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

public class GetListRecetaFabricacionVWQueryProfile : Profile
{
    public GetListRecetaFabricacionVWQueryProfile() =>
        CreateMap<RecetaFabricacionVW, GetListRecetaFabricacionVWQueryResponse>()
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

