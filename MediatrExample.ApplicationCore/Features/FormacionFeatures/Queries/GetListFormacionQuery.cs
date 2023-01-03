using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MediatrExample.ApplicationCore.Features.FormacionFeatures.Queries;

public class GetListFormacionQuery : IRequest<List<GetListFormacionQueryResponse>>
{

}

public class GetListFormacionQueryHandler : IRequestHandler<GetListFormacionQuery, List<GetListFormacionQueryResponse>>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetListFormacionQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<GetListFormacionQueryResponse>> Handle(GetListFormacionQuery request, CancellationToken cancellationToken) =>
        _context.Formaciones
            .AsNoTracking()
            .ProjectTo<GetListFormacionQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
}

public class GetListFormacionQueryResponse
{
    public string FormacionId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedidaAngulo { get; set; } = default!;
    public decimal RangoAnguloMinimo { get; set; }
    public decimal RangoAnguloMaximo { get; set; }
    public string UnidadMedidaAltura { get; set; } = default!;
    public decimal RangoAlturaMinimo { get; set; }
    public decimal RangoAlturaMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
    public string AnguloMinimoUnidad { get; set; } = default!;
    public string AnguloMaximoUnidad { get; set; } = default!;
    public string AlturaMinimoUnidad { get; set; } = default!;
    public string AlturaMaximoUnidad { get; set; } = default!;
}

public class GetListFormacionQueryProfile : Profile
{
    public GetListFormacionQueryProfile() =>
        CreateMap<Formacion, GetListFormacionQueryResponse>()
            .ForMember(dest =>
                dest.FormacionId,
                opt => opt.MapFrom(mf => mf.FormacionId.ToHashId()))
            .ForMember(dest =>
                dest.AnguloMinimoUnidad,
                opt => opt.MapFrom(mf => $"{mf.RangoAnguloMinimo}{mf.UnidadMedidaAngulo}"))
            .ForMember(dest =>
                dest.AnguloMaximoUnidad,
                opt => opt.MapFrom(mf => $"{mf.RangoAnguloMaximo}{mf.UnidadMedidaAngulo}"))
            .ForMember(dest =>
                dest.AlturaMinimoUnidad,
                opt => opt.MapFrom(mf => $"{mf.RangoAlturaMinimo}{mf.UnidadMedidaAngulo}"))
            .ForMember(dest =>
                dest.AlturaMaximoUnidad,
                opt => opt.MapFrom(mf => $"{mf.RangoAlturaMaximo}{mf.UnidadMedidaAngulo}"));

}
