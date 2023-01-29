using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MediatrExample.ApplicationCore.Features.IndicadorPrensaFeatures.Queries;

public class GetListIndicadorPrensaQuery : IRequest<List<GetListIndicadorPrensaQueryResponse>>
{

}

public class GetListIndicadorPrensaQueryHandler : IRequestHandler<GetListIndicadorPrensaQuery, List<GetListIndicadorPrensaQueryResponse>>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetListIndicadorPrensaQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<GetListIndicadorPrensaQueryResponse>> Handle(GetListIndicadorPrensaQuery request, CancellationToken cancellationToken) =>
        _context.IndicadoresPrensa
            .AsNoTracking()
            .ProjectTo<GetListIndicadorPrensaQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
}

public class GetListIndicadorPrensaQueryResponse
{
    public string IndicadorPrensaId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public int TipoIndicadorPrensaId { get; set; }
    public string TipoIndicadorPrensaNombre { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

public class GetListIndicadorPrensaQueryProfile : Profile
{
    public GetListIndicadorPrensaQueryProfile() =>
        CreateMap<IndicadorPrensa, GetListIndicadorPrensaQueryResponse>()
            .ForMember(dest =>
                dest.IndicadorPrensaId,
                opt => opt.MapFrom(mf => mf.IndicadorPrensaId.ToHashId()))
            .ForMember(dest =>
                dest.TipoIndicadorPrensaNombre,
                opt => opt.MapFrom(mf => mf.TipoIndicadorPrensa.NombreVariable));

}
