using AutoMapper;
using MediatR;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;

namespace MediatrExample.ApplicationCore.Features.TipoRecetaFeatures.Queries;

public class NewTipoRecetaQuery : IRequest<NewTipoRecetaQueryResponse>
{

}

public class NewTipoRecetaQueryHandler : IRequestHandler<NewTipoRecetaQuery, NewTipoRecetaQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;
    readonly string _connectionString = "";

    public NewTipoRecetaQueryHandler(MyAppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<NewTipoRecetaQueryResponse> Handle(NewTipoRecetaQuery request, CancellationToken cancellationToken)
    {
        var responseTipoReceta = new NewTipoRecetaQueryResponse();

        return responseTipoReceta;
    }
}

public class NewTipoRecetaQueryResponse
{
    public string NombreVariable { get; set; } = "";
    public string Descripcion { get; set; } = "";
    public bool Estado { get; set; } = true;
}

public class NewTipoRecetaQueryProfile : Profile
{
    public NewTipoRecetaQueryProfile() =>
        CreateMap<TipoReceta, NewTipoRecetaQueryResponse>();

}