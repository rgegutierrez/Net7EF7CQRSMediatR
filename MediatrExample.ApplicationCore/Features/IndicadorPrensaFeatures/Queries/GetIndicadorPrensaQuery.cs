using AutoMapper;
using MediatR;
using MediatrExample.ApplicationCore.Common.Exceptions;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;


namespace MediatrExample.ApplicationCore.Features.IndicadorPrensaFeatures.Queries;

public class GetIndicadorPrensaQuery : IRequest<GetIndicadorPrensaQueryResponse>
{
    public string IndicadorPrensaId { get; set; }
}

public class GetIndicadorPrensaQueryHandler : IRequestHandler<GetIndicadorPrensaQuery, GetIndicadorPrensaQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetIndicadorPrensaQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetIndicadorPrensaQueryResponse> Handle(GetIndicadorPrensaQuery request, CancellationToken cancellationToken)
    {
        var obj = await _context.IndicadoresPrensa.FindAsync(request.IndicadorPrensaId.FromHashId());

        if (obj is null)
        {
            throw new NotFoundException(nameof(Product), request.IndicadorPrensaId);
        }

        var responseIndicadorPrensa = _mapper.Map<GetIndicadorPrensaQueryResponse>(obj);
        responseIndicadorPrensa.LstTipoIndicadorPrensa = _context.TipoIndicadoresPrensa.ToList();

        return responseIndicadorPrensa;
    }
}

public class GetIndicadorPrensaQueryResponse
{
    public string IndicadorPrensaId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public int TipoIndicadorPrensaId { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
    public List<TipoIndicadorPrensa> LstTipoIndicadorPrensa { get; set; }
}

public class GetIndicadorPrensaQueryProfile : Profile
{
    public GetIndicadorPrensaQueryProfile() =>
        CreateMap<IndicadorPrensa, GetIndicadorPrensaQueryResponse>()
            .ForMember(dest =>
                dest.IndicadorPrensaId,
                opt => opt.MapFrom(mf => mf.IndicadorPrensaId.ToHashId()));

}