using AutoMapper;
using Dapper;
using MediatR;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MediatrExample.ApplicationCore.Features.FormacionFeatures.Queries;

public class NewFormacionQuery : IRequest<NewFormacionQueryResponse>
{

}

public class NewFormacionQueryHandler : IRequestHandler<NewFormacionQuery, NewFormacionQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;
    readonly string _connectionString = "";

    public NewFormacionQueryHandler(MyAppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<NewFormacionQueryResponse> Handle(NewFormacionQuery request, CancellationToken cancellationToken)
    {
        var responseFormacion = new NewFormacionQueryResponse();

        using IDbConnection con = new SqlConnection(_connectionString);
        if (con.State == ConnectionState.Closed) con.Open();
        var _lstUnidadMedida = await con.QueryAsync<UnidadMedida>(
            "[trzreceta].[GetListUnidadMedida]",
            new { },
            commandType: CommandType.StoredProcedure
            );

        if (_lstUnidadMedida != null && _lstUnidadMedida.Any())
        {
            responseFormacion.Unidades = _lstUnidadMedida.ToList();
        }

        return responseFormacion;
    }
}

public class NewFormacionQueryResponse
{
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedidaAngulo { get; set; } = "º";
    public decimal RangoAnguloMinimo { get; set; }
    public decimal RangoAnguloMaximo { get; set; }
    public string UnidadMedidaAltura { get; set; } = "mm";
    public decimal RangoAlturaMinimo { get; set; }
    public decimal RangoAlturaMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; } = true;
    public List<UnidadMedida>? Unidades { get; set; }
}

public class NewFormacionQueryProfile : Profile
{
    public NewFormacionQueryProfile() =>
        CreateMap<Formacion, NewFormacionQueryResponse>();

}