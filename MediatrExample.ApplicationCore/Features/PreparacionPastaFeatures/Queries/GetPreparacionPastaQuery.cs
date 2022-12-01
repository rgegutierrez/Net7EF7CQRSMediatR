using AutoMapper;
using MediatR;
using MediatrExample.ApplicationCore.Common.Exceptions;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;


namespace MediatrExample.ApplicationCore.Features.PreparacionPastaFeatures.Queries;

public class GetPreparacionPastaQuery : IRequest<GetPreparacionPastaQueryResponse>
{
    public string PreparacionPastaId { get; set; }
}

public class GetPreparacionPastaQueryHandler : IRequestHandler<GetPreparacionPastaQuery, GetPreparacionPastaQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetPreparacionPastaQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetPreparacionPastaQueryResponse> Handle(GetPreparacionPastaQuery request, CancellationToken cancellationToken)
    {
        var materiaPrima = await _context.PreparacionPastas.FindAsync(request.PreparacionPastaId.FromHashId());

        if (materiaPrima is null)
        {
            throw new NotFoundException(nameof(PreparacionPasta), request.PreparacionPastaId);
        }

        return _mapper.Map<GetPreparacionPastaQueryResponse>(materiaPrima);
    }
}

public class GetPreparacionPastaQueryResponse
{
    public string PreparacionPastaId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public int ValorMinimo { get; set; }
    public int ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

public class GetPreparacionPastaQueryProfile : Profile
{
    public GetPreparacionPastaQueryProfile() =>
        CreateMap<PreparacionPasta, GetPreparacionPastaQueryResponse>()
            .ForMember(dest =>
                dest.PreparacionPastaId,
                opt => opt.MapFrom(mf => mf.PreparacionPastaId.ToHashId()));

}