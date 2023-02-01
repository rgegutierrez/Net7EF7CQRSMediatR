using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MediatrExample.ApplicationCore.Features.PreparacionPastaFeatures.Queries;

public class GetListPreparacionPastaQuery : IRequest<List<GetListPreparacionPastaQueryResponse>>
{

}

public class GetListPreparacionPastaQueryHandler : IRequestHandler<GetListPreparacionPastaQuery, List<GetListPreparacionPastaQueryResponse>>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetListPreparacionPastaQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<GetListPreparacionPastaQueryResponse>> Handle(GetListPreparacionPastaQuery request, CancellationToken cancellationToken) =>
        _context.PreparacionPastas
            .AsNoTracking()
            .ProjectTo<GetListPreparacionPastaQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
}

public class GetListPreparacionPastaQueryResponse
{
    public string PreparacionPastaId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
    public string ValorMinimoStr { get; set; } = default!;
    public string ValorMaximoStr { get; set; } = default!;
}

public class GetListPreparacionPastaQueryProfile : Profile
{
    public GetListPreparacionPastaQueryProfile() =>
        CreateMap<PreparacionPasta, GetListPreparacionPastaQueryResponse>()
            .ForMember(dest =>
                dest.ValorMinimoStr,
                opt => opt.MapFrom(mf => mf.ValorMinimo.FromDotToComma()))
            .ForMember(dest =>
                dest.ValorMaximoStr,
                opt => opt.MapFrom(mf => mf.ValorMaximo.FromDotToComma()))
            .ForMember(dest =>
                dest.PreparacionPastaId,
                opt => opt.MapFrom(mf => mf.PreparacionPastaId.ToHashId()));

}
