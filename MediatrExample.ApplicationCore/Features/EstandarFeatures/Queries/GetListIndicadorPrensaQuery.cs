using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MediatrExample.ApplicationCore.Features.EstandarFeatures.Queries;

public class GetListEstandarQuery : IRequest<List<GetListEstandarQueryResponse>>
{

}

public class GetListEstandarQueryHandler : IRequestHandler<GetListEstandarQuery, List<GetListEstandarQueryResponse>>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetListEstandarQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<GetListEstandarQueryResponse>> Handle(GetListEstandarQuery request, CancellationToken cancellationToken) =>
        _context.Estandares
            .AsNoTracking()
            .ProjectTo<GetListEstandarQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
}

public class GetListEstandarQueryResponse
{
    public string EstandarId { get; set; } = default!;
    public int ClienteId { get; set; }
    public int TipoPapelId { get; set; }
    public int ValorFisicoPieMaquinaId { get; set; }
    public int ValorFisicoPieMaquinaNombre { get; set; }
    public decimal ValorMinimo { get; set; }
    public decimal ValorPromedio { get; set; }
    public decimal ValorMaximo { get; set; }
}

public class GetListEstandarQueryProfile : Profile
{
    public GetListEstandarQueryProfile() =>
        CreateMap<Estandar, GetListEstandarQueryResponse>()
            .ForMember(dest =>
                dest.EstandarId,
                opt => opt.MapFrom(mf => mf.EstandarId.ToHashId()))
            .ForMember(dest =>
                dest.ValorFisicoPieMaquinaNombre,
                opt => opt.MapFrom(mf => mf.ValorFisicoPieMaquina.NombreVariable));

}
