using AutoMapper;
using MediatR;
using MediatrExample.ApplicationCore.Common.Exceptions;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;


namespace MediatrExample.ApplicationCore.Features.LineaProduccionFeatures.Queries;

public class GetLineaProduccionQuery : IRequest<GetLineaProduccionQueryResponse>
{
    public string LineaProduccionId { get; set; }
}

public class GetLineaProduccionQueryHandler : IRequestHandler<GetLineaProduccionQuery, GetLineaProduccionQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetLineaProduccionQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetLineaProduccionQueryResponse> Handle(GetLineaProduccionQuery request, CancellationToken cancellationToken)
    {
        var obj = await _context.LineasProduccion.FindAsync(request.LineaProduccionId.FromHashId());

        if (obj is null)
        {
            throw new NotFoundException(nameof(Product), request.LineaProduccionId);
        }

        return _mapper.Map<GetLineaProduccionQueryResponse>(obj);
    }
}

public class GetLineaProduccionQueryResponse
{
    public string LineaProduccionId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string Descripcion { get; set; } = default!;
    public bool Estado { get; set; }
}

public class GetLineaProduccionQueryProfile : Profile
{
    public GetLineaProduccionQueryProfile() =>
        CreateMap<LineaProduccion, GetLineaProduccionQueryResponse>()
            .ForMember(dest =>
                dest.LineaProduccionId,
                opt => opt.MapFrom(mf => mf.LineaProduccionId.ToHashId()));

}