using AutoMapper;
using MediatR;
using MediatrExample.ApplicationCore.Common.Exceptions;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Domain.View;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;


namespace MediatrExample.ApplicationCore.Features.EstandarFeatures.Queries;

public class GetEstandarQuery : IRequest<GetEstandarQueryResponse>
{
    public string EstandarId { get; set; }
}

public class GetEstandarQueryHandler : IRequestHandler<GetEstandarQuery, GetEstandarQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetEstandarQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetEstandarQueryResponse> Handle(GetEstandarQuery request, CancellationToken cancellationToken)
    {
        var obj = await _context.Estandares.FindAsync(request.EstandarId.FromHashId());

        if (obj is null)
        {
            throw new NotFoundException(nameof(Product), request.EstandarId);
        }

        var responseEstandar = _mapper.Map<GetEstandarQueryResponse>(obj);
        responseEstandar.LstValorFisicoPieMaquina = _context.ValoresFisicoPieMaquina.ToList();
        responseEstandar.LstRecetaCliente = _context.RecetaClientes.ToList();
        responseEstandar.LstRecetaTipoPapel = _context.RecetaTipoPapeles.ToList();

        return responseEstandar;
    }
}

public class GetEstandarQueryResponse
{
    public string EstandarId { get; set; } = default!;
    public int ClienteId { get; set; }
    public int TipoPapelId { get; set; }
    public int ValorFisicoPieMaquinaId { get; set; }
    public decimal ValorMinimo { get; set; }
    public decimal ValorPromedio { get; set; }
    public decimal ValorMaximo { get; set; }
    public List<ValorFisicoPieMaquina> LstValorFisicoPieMaquina { get; set; }
    public List<RecetaCliente> LstRecetaCliente { get; set; }
    public List<RecetaTipoPapel> LstRecetaTipoPapel { get; set; }
}

public class GetEstandarQueryProfile : Profile
{
    public GetEstandarQueryProfile() =>
        CreateMap<Estandar, GetEstandarQueryResponse>()
            .ForMember(dest =>
                dest.EstandarId,
                opt => opt.MapFrom(mf => mf.EstandarId.ToHashId()));

}