using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MediatrExample.ApplicationCore.Features.LineaProduccionFeatures.Queries;

public class GetListLineaProduccionQuery : IRequest<List<GetListLineaProduccionQueryResponse>>
{

}

public class GetListLineaProduccionQueryHandler : IRequestHandler<GetListLineaProduccionQuery, List<GetListLineaProduccionQueryResponse>>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetListLineaProduccionQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<GetListLineaProduccionQueryResponse>> Handle(GetListLineaProduccionQuery request, CancellationToken cancellationToken) =>
        _context.LineasProduccion
            .AsNoTracking()
            .ProjectTo<GetListLineaProduccionQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
}

public class GetListLineaProduccionQueryResponse
{
    public string LineaProduccionId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string Descripcion { get; set; } = default!;
    public bool Estado { get; set; }
}

public class GetListLineaProduccionQueryProfile : Profile
{
    public GetListLineaProduccionQueryProfile() =>
        CreateMap<LineaProduccion, GetListLineaProduccionQueryResponse>()
            .ForMember(dest =>
                dest.LineaProduccionId,
                opt => opt.MapFrom(mf => mf.LineaProduccionId.ToHashId()));

}
