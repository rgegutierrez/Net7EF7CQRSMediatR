using AutoMapper;
using MediatR;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;

namespace MediatrExample.ApplicationCore.Features.IndicadorVacioFeatures.Queries;

public class NewIndicadorVacioQuery : IRequest<NewIndicadorVacioQueryResponse>
{

}

public class NewIndicadorVacioQueryHandler : IRequestHandler<NewIndicadorVacioQuery, NewIndicadorVacioQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;
    readonly string _connectionString = "";

    public NewIndicadorVacioQueryHandler(MyAppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<NewIndicadorVacioQueryResponse> Handle(NewIndicadorVacioQuery request, CancellationToken cancellationToken)
    {
        var responseIndicadorVacio = new NewIndicadorVacioQueryResponse
        {
            LstTipoIndicadorVacio = _context.TipoIndicadoresVacio.ToList()
        };

        return responseIndicadorVacio;
    }
}

public class NewIndicadorVacioQueryResponse
{
    public string NombreVariable { get; set; } = default!;
    public int TipoIndicadorVacioId { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; } = true;
    public List<TipoIndicadorVacio> LstTipoIndicadorVacio { get;set; }
}

public class NewIndicadorVacioQueryProfile : Profile
{
    public NewIndicadorVacioQueryProfile() =>
        CreateMap<IndicadorVacio, NewIndicadorVacioQueryResponse>();

}