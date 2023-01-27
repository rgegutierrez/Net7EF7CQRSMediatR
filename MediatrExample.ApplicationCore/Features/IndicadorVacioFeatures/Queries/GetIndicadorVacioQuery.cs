using AutoMapper;
using MediatR;
using MediatrExample.ApplicationCore.Common.Exceptions;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;


namespace MediatrExample.ApplicationCore.Features.IndicadorVacioFeatures.Queries;

public class GetIndicadorVacioQuery : IRequest<GetIndicadorVacioQueryResponse>
{
    public string IndicadorVacioId { get; set; }
}

public class GetIndicadorVacioQueryHandler : IRequestHandler<GetIndicadorVacioQuery, GetIndicadorVacioQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetIndicadorVacioQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetIndicadorVacioQueryResponse> Handle(GetIndicadorVacioQuery request, CancellationToken cancellationToken)
    {
        var obj = await _context.IndicadoresVacio.FindAsync(request.IndicadorVacioId.FromHashId());

        if (obj is null)
        {
            throw new NotFoundException(nameof(Product), request.IndicadorVacioId);
        }

        var responseIndicadorVacio = _mapper.Map<GetIndicadorVacioQueryResponse>(obj);
        responseIndicadorVacio.LstTipoIndicadorVacio = _context.TipoIndicadoresVacio.ToList();

        return responseIndicadorVacio;
    }
}

public class GetIndicadorVacioQueryResponse
{
    public string IndicadorVacioId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public int TipoIndicadorVacioId { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
    public List<TipoIndicadorVacio> LstTipoIndicadorVacio { get; set; }
}

public class GetIndicadorVacioQueryProfile : Profile
{
    public GetIndicadorVacioQueryProfile() =>
        CreateMap<IndicadorVacio, GetIndicadorVacioQueryResponse>()
            .ForMember(dest =>
                dest.IndicadorVacioId,
                opt => opt.MapFrom(mf => mf.IndicadorVacioId.ToHashId()));

}