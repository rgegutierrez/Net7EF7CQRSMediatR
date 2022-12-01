using AutoMapper;
using MediatR;
using MediatrExample.ApplicationCore.Common.Exceptions;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;


namespace MediatrExample.ApplicationCore.Features.MateriaPrimaFeatures.Queries;

public class GetPreparacionPastaQuery : IRequest<GetMateriaPrimaQueryResponse>
{
    public string MateriaPrimaId { get; set; }
}

public class GetMateriaPrimaQueryHandler : IRequestHandler<GetPreparacionPastaQuery, GetMateriaPrimaQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetMateriaPrimaQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetMateriaPrimaQueryResponse> Handle(GetPreparacionPastaQuery request, CancellationToken cancellationToken)
    {
        var materiaPrima = await _context.MateriasPrimas.FindAsync(request.MateriaPrimaId.FromHashId());

        if (materiaPrima is null)
        {
            throw new NotFoundException(nameof(MateriaPrima), request.MateriaPrimaId);
        }

        return _mapper.Map<GetMateriaPrimaQueryResponse>(materiaPrima);
    }
}

public class GetMateriaPrimaQueryResponse
{
    public string MateriaPrimaId { get; set; } = default!;
    public string CodigoSap { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public int ValorMinimo { get; set; }
    public int ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

public class GetMateriaPrimaQueryProfile : Profile
{
    public GetMateriaPrimaQueryProfile() =>
        CreateMap<MateriaPrima, GetMateriaPrimaQueryResponse>()
            .ForMember(dest =>
                dest.MateriaPrimaId,
                opt => opt.MapFrom(mf => mf.MateriaPrimaId.ToHashId()));

}