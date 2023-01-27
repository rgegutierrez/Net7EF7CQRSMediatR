using AutoMapper;
using MediatR;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;

namespace MediatrExample.ApplicationCore.Features.IndicadorPrensaFeatures.Queries;

public class NewIndicadorPrensaQuery : IRequest<NewIndicadorPrensaQueryResponse>
{

}

public class NewIndicadorPrensaQueryHandler : IRequestHandler<NewIndicadorPrensaQuery, NewIndicadorPrensaQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;
    readonly string _connectionString = "";

    public NewIndicadorPrensaQueryHandler(MyAppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<NewIndicadorPrensaQueryResponse> Handle(NewIndicadorPrensaQuery request, CancellationToken cancellationToken)
    {
        var responseIndicadorPrensa = new NewIndicadorPrensaQueryResponse
        {
            LstTipoIndicadorPrensa = _context.TipoIndicadoresPrensa.ToList()
        };

        return responseIndicadorPrensa;
    }
}

public class NewIndicadorPrensaQueryResponse
{
    public string NombreVariable { get; set; } = default!;
    public int TipoIndicadorPrensaId { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; } = true;
    public List<TipoIndicadorPrensa> LstTipoIndicadorPrensa { get;set; }
}

public class NewIndicadorPrensaQueryProfile : Profile
{
    public NewIndicadorPrensaQueryProfile() =>
        CreateMap<IndicadorPrensa, NewIndicadorPrensaQueryResponse>();

}