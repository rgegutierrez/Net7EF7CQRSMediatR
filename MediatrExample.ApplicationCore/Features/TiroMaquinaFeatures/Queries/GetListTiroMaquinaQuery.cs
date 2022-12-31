using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MediatrExample.ApplicationCore.Features.TiroMaquinaFeatures.Queries;

public class GetListTiroMaquinaQuery : IRequest<List<GetListTiroMaquinaQueryResponse>>
{

}

public class GetListTiroMaquinaQueryHandler : IRequestHandler<GetListTiroMaquinaQuery, List<GetListTiroMaquinaQueryResponse>>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetListTiroMaquinaQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<GetListTiroMaquinaQueryResponse>> Handle(GetListTiroMaquinaQuery request, CancellationToken cancellationToken) =>
        _context.TirosMaquina
            .AsNoTracking()
            .ProjectTo<GetListTiroMaquinaQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
    
}

public class GetListTiroMaquinaQueryResponse
{
    public string TiroMaquinaId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
    public string StrValorMinimo { get; set; } = default!;
    public string StrValorMaximo { get; set; } = default!;
}

public class GetListTiroMaquinaQueryProfile : Profile
{
    public GetListTiroMaquinaQueryProfile() =>
        CreateMap<TiroMaquina, GetListTiroMaquinaQueryResponse>()
            .ForMember(dest =>
                dest.StrValorMinimo,
                opt => opt.MapFrom(mf => $"{mf.ValorMinimo}{mf.UnidadMedida}"))
            .ForMember(dest =>
                dest.StrValorMaximo,
                opt => opt.MapFrom(mf => $"{mf.ValorMaximo}{mf.UnidadMedida}"))
            .ForMember(dest =>
                dest.TiroMaquinaId,
                opt => opt.MapFrom(mf => mf.TiroMaquinaId.ToHashId()));

}
