using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MediatrExample.ApplicationCore.Features.IndicadorSecadorFeatures.Queries;

public class GetListIndicadorSecadorQuery : IRequest<List<GetListIndicadorSecadorQueryResponse>>
{

}

public class GetListIndicadorSecadorQueryHandler : IRequestHandler<GetListIndicadorSecadorQuery, List<GetListIndicadorSecadorQueryResponse>>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetListIndicadorSecadorQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<GetListIndicadorSecadorQueryResponse>> Handle(GetListIndicadorSecadorQuery request, CancellationToken cancellationToken) =>
        _context.IndicadoresSecador
            .AsNoTracking()
            .ProjectTo<GetListIndicadorSecadorQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
}

public class GetListIndicadorSecadorQueryResponse
{
    public string IndicadorSecadorId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public int TipoIndicadorSecadorId { get; set; }
    public string TipoIndicadorSecadorNombre { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
    public string ValorMinimoStr { get; set; }
    public string ValorMaximoStr { get; set; }
}

public class GetListIndicadorSecadorQueryProfile : Profile
{
    public GetListIndicadorSecadorQueryProfile() =>
        CreateMap<IndicadorSecador, GetListIndicadorSecadorQueryResponse>()
            .ForMember(dest =>
                dest.IndicadorSecadorId,
                opt => opt.MapFrom(mf => mf.IndicadorSecadorId.ToHashId()))
            .ForMember(dest =>
                dest.TipoIndicadorSecadorNombre,
                opt => opt.MapFrom(mf => mf.TipoIndicadorSecador.NombreVariable))
            .ForMember(dest =>
                dest.ValorMinimoStr,
                opt => opt.MapFrom(mf => mf.ValorMinimo.FromDotToComma()))
            .ForMember(dest =>
                dest.ValorMaximoStr,
                opt => opt.MapFrom(mf => mf.ValorMaximo.FromDotToComma()));

}
