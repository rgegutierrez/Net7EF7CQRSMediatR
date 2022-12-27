using AutoMapper;
using MediatR;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;

namespace MediatrExample.ApplicationCore.Features.LineaProduccionFeatures.Queries;

public class NewLineaProduccionQuery : IRequest<NewLineaProduccionQueryResponse>
{

}

public class NewLineaProduccionQueryHandler : IRequestHandler<NewLineaProduccionQuery, NewLineaProduccionQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;
    readonly string _connectionString = "";

    public NewLineaProduccionQueryHandler(MyAppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<NewLineaProduccionQueryResponse> Handle(NewLineaProduccionQuery request, CancellationToken cancellationToken)
    {
        var responseLineaProduccion = new NewLineaProduccionQueryResponse();

        return responseLineaProduccion;
    }
}

public class NewLineaProduccionQueryResponse
{
    public string NombreVariable { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public bool Estado { get; set; } = true;
}

public class NewLineaProduccionQueryProfile : Profile
{
    public NewLineaProduccionQueryProfile() =>
        CreateMap<LineaProduccion, NewLineaProduccionQueryResponse>();

}