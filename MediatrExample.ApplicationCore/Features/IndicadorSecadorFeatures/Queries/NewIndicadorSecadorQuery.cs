using AutoMapper;
using MediatR;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;

namespace MediatrExample.ApplicationCore.Features.IndicadorSecadorFeatures.Queries;

public class NewIndicadorSecadorQuery : IRequest<NewIndicadorSecadorQueryResponse>
{

}

public class NewIndicadorSecadorQueryHandler : IRequestHandler<NewIndicadorSecadorQuery, NewIndicadorSecadorQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;
    readonly string _connectionString = "";

    public NewIndicadorSecadorQueryHandler(MyAppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<NewIndicadorSecadorQueryResponse> Handle(NewIndicadorSecadorQuery request, CancellationToken cancellationToken)
    {
        var responseIndicadorSecador = new NewIndicadorSecadorQueryResponse
        {
            LstTipoIndicadorSecador = _context.TipoIndicadoresSecador.OrderBy(o => o.NombreVariable).ToList(),
            Unidades = _context.UnidadesMedida.OrderBy(o => o.NombreVariable).ToList()
        };

        return responseIndicadorSecador;
    }
}

public class NewIndicadorSecadorQueryResponse
{
    public string NombreVariable { get; set; } = default!;
    public int TipoIndicadorSecadorId { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; } = true;
    public List<TipoIndicadorSecador> LstTipoIndicadorSecador { get;set; }
    public List<UnidadMedida> Unidades { get; set; }
}

public class NewIndicadorSecadorQueryProfile : Profile
{
    public NewIndicadorSecadorQueryProfile() =>
        CreateMap<IndicadorSecador, NewIndicadorSecadorQueryResponse>();

}