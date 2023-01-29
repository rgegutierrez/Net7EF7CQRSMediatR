using AutoMapper;
using MediatR;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Domain.View;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;

namespace MediatrExample.ApplicationCore.Features.EstandarFeatures.Queries;

public class NewEstandarQuery : IRequest<NewEstandarQueryResponse>
{

}

public class NewEstandarQueryHandler : IRequestHandler<NewEstandarQuery, NewEstandarQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;
    readonly string _connectionString = "";

    public NewEstandarQueryHandler(MyAppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<NewEstandarQueryResponse> Handle(NewEstandarQuery request, CancellationToken cancellationToken)
    {
        var responseEstandar = new NewEstandarQueryResponse
        {
            LstValorFisicoPieMaquina = _context.ValoresFisicoPieMaquina.ToList(),
            LstRecetaCliente = _context.RecetaClientes.ToList(),
            LstRecetaTipoPapel = _context.RecetaTipoPapeles.ToList()
        };

        return responseEstandar;
    }
}

public class NewEstandarQueryResponse
{
    public string ClienteId { get; set; }
    public string TipoPapelId { get; set; }
    public int ValorFisicoPieMaquinaId { get; set; }
    public decimal ValorMinimo { get; set; }
    public decimal ValorPromedio { get; set; }
    public decimal ValorMaximo { get; set; }
    public List<ValorFisicoPieMaquina> LstValorFisicoPieMaquina { get;set; }
    public List<RecetaCliente> LstRecetaCliente { get; set; }
    public List<RecetaTipoPapel> LstRecetaTipoPapel { get; set; }
}

public class NewEstandarQueryProfile : Profile
{
    public NewEstandarQueryProfile() =>
        CreateMap<Estandar, NewEstandarQueryResponse>();

}