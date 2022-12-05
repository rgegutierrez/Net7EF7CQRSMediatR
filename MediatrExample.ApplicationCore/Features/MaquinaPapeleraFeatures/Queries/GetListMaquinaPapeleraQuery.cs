using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MediatrExample.ApplicationCore.Features.MaquinaPapeleraFeatures.Queries;

public class GetListMaquinaPapeleraQuery : IRequest<List<GetListMaquinaPapeleraQueryResponse>>
{

}

public class GetListMaquinaPapeleraQueryHandler : IRequestHandler<GetListMaquinaPapeleraQuery, List<GetListMaquinaPapeleraQueryResponse>>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetListMaquinaPapeleraQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<GetListMaquinaPapeleraQueryResponse>> Handle(GetListMaquinaPapeleraQuery request, CancellationToken cancellationToken) =>
        _context.MaquinasPapeleras
            .AsNoTracking()
            .ProjectTo<GetListMaquinaPapeleraQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
}

public class GetListMaquinaPapeleraQueryResponse
{
    public string MaquinaPapeleraId { get; set; } = default!;
    public int Orden { get; set; }
    public string NombreVariable { get; set; } = default!;
    public int LineaProduccion { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public int ValorMinimo { get; set; }
    public int ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool ModoIngreso { get; set; }
    public string FormulaCalculo { get; set; } = default!;
    public bool Estado { get; set; }
    public string StrValorMinimo { get; set; } = default!;
    public string StrValorMaximo { get; set; } = default!;
}

public class GetListMaquinaPapeleraQueryProfile : Profile
{
    public GetListMaquinaPapeleraQueryProfile() =>
        CreateMap<MaquinaPapelera, GetListMaquinaPapeleraQueryResponse>()
            .ForMember(dest =>
                dest.StrValorMinimo,
                opt => opt.MapFrom(mf => $"{mf.ValorMinimo}{mf.UnidadMedida}"))
            .ForMember(dest =>
                dest.StrValorMaximo,
                opt => opt.MapFrom(mf => $"{mf.ValorMaximo}{mf.UnidadMedida}"))
            .ForMember(dest =>
                dest.MaquinaPapeleraId,
                opt => opt.MapFrom(mf => mf.MaquinaPapeleraId.ToHashId()));

}
