using AutoMapper;
using MediatR;
using MediatrExample.ApplicationCore.Common.Exceptions;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;


namespace MediatrExample.ApplicationCore.Features.IndicadorSecadorFeatures.Queries;

public class GetIndicadorSecadorQuery : IRequest<GetIndicadorSecadorQueryResponse>
{
    public string IndicadorSecadorId { get; set; }
}

public class GetIndicadorSecadorQueryHandler : IRequestHandler<GetIndicadorSecadorQuery, GetIndicadorSecadorQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetIndicadorSecadorQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetIndicadorSecadorQueryResponse> Handle(GetIndicadorSecadorQuery request, CancellationToken cancellationToken)
    {
        var obj = await _context.IndicadoresSecador.FindAsync(request.IndicadorSecadorId.FromHashId());

        if (obj is null)
        {
            throw new NotFoundException(nameof(Product), request.IndicadorSecadorId);
        }

        var responseIndicadorSecador = _mapper.Map<GetIndicadorSecadorQueryResponse>(obj);
        responseIndicadorSecador.LstTipoIndicadorSecador = _context.TipoIndicadoresSecador.ToList();

        return responseIndicadorSecador;
    }
}

public class GetIndicadorSecadorQueryResponse
{
    public string IndicadorSecadorId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public int TipoIndicadorSecadorId { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
    public List<TipoIndicadorSecador> LstTipoIndicadorSecador { get; set; }
}

public class GetIndicadorSecadorQueryProfile : Profile
{
    public GetIndicadorSecadorQueryProfile() =>
        CreateMap<IndicadorSecador, GetIndicadorSecadorQueryResponse>()
            .ForMember(dest =>
                dest.IndicadorSecadorId,
                opt => opt.MapFrom(mf => mf.IndicadorSecadorId.ToHashId()));

}