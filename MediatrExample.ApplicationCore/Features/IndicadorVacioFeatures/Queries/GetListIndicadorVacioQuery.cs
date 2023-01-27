using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MediatrExample.ApplicationCore.Features.IndicadorVacioFeatures.Queries;

public class GetListIndicadorVacioQuery : IRequest<List<GetListIndicadorVacioQueryResponse>>
{

}

public class GetListIndicadorVacioQueryHandler : IRequestHandler<GetListIndicadorVacioQuery, List<GetListIndicadorVacioQueryResponse>>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetListIndicadorVacioQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<GetListIndicadorVacioQueryResponse>> Handle(GetListIndicadorVacioQuery request, CancellationToken cancellationToken) =>
        _context.IndicadoresVacio
            .AsNoTracking()
            .ProjectTo<GetListIndicadorVacioQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
}

public class GetListIndicadorVacioQueryResponse
{
    public string IndicadorVacioId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public int TipoIndicadorVacioId { get; set; }
    public string TipoIndicadorVacioNombre { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

public class GetListIndicadorVacioQueryProfile : Profile
{
    public GetListIndicadorVacioQueryProfile() =>
        CreateMap<IndicadorVacio, GetListIndicadorVacioQueryResponse>()
            .ForMember(dest =>
                dest.IndicadorVacioId,
                opt => opt.MapFrom(mf => mf.IndicadorVacioId.ToHashId()))
            .ForMember(dest =>
                dest.TipoIndicadorVacioNombre,
                opt => opt.MapFrom(mf => mf.TipoIndicadorVacio.NombreVariable));

}
