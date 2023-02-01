using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MediatrExample.ApplicationCore.Features.ValorFisicoPieMaquinaFeatures.Queries;

public class GetListValorFisicoPieMaquinaQuery : IRequest<List<GetListValorFisicoPieMaquinaQueryResponse>>
{

}

public class GetListValorFisicoPieMaquinaQueryHandler : IRequestHandler<GetListValorFisicoPieMaquinaQuery, List<GetListValorFisicoPieMaquinaQueryResponse>>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetListValorFisicoPieMaquinaQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<GetListValorFisicoPieMaquinaQueryResponse>> Handle(GetListValorFisicoPieMaquinaQuery request, CancellationToken cancellationToken) =>
        _context.ValoresFisicoPieMaquina
            .AsNoTracking()
            .ProjectTo<GetListValorFisicoPieMaquinaQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
    
}

public class GetListValorFisicoPieMaquinaQueryResponse
{
    public string ValorFisicoPieMaquinaId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
    public string ValorMinimoStr { get; set; } = default!;
    public string ValorMaximoStr { get; set; } = default!;
}

public class GetListValorFisicoPieMaquinaQueryProfile : Profile
{
    public GetListValorFisicoPieMaquinaQueryProfile() =>
        CreateMap<ValorFisicoPieMaquina, GetListValorFisicoPieMaquinaQueryResponse>()
            .ForMember(dest =>
                dest.ValorMinimoStr,
                opt => opt.MapFrom(mf => mf.ValorMinimo.FromDotToComma()))
            .ForMember(dest =>
                dest.ValorMaximoStr,
                opt => opt.MapFrom(mf => mf.ValorMaximo.FromDotToComma()))
            .ForMember(dest =>
                dest.ValorFisicoPieMaquinaId,
                opt => opt.MapFrom(mf => mf.ValorFisicoPieMaquinaId.ToHashId()));

}
