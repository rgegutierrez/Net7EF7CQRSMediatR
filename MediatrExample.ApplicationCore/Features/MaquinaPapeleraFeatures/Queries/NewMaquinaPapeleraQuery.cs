using AutoMapper;
using Dapper;
using MediatR;
using MediatrExample.ApplicationCore.Common.Exceptions;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MediatrExample.ApplicationCore.Features.MaquinaPapeleraFeatures.Queries;

public class NewMaquinaPapeleraQuery : IRequest<NewMaquinaPapeleraQueryResponse>
{

}

public class NewMaquinaPapeleraQueryHandler : IRequestHandler<NewMaquinaPapeleraQuery, NewMaquinaPapeleraQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;
    readonly string _connectionString = "";

    public NewMaquinaPapeleraQueryHandler(MyAppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<NewMaquinaPapeleraQueryResponse> Handle(NewMaquinaPapeleraQuery request, CancellationToken cancellationToken)
    {
        var responseMaquinaPapelera = new NewMaquinaPapeleraQueryResponse();

        using IDbConnection con = new SqlConnection(_connectionString);
        if (con.State == ConnectionState.Closed) con.Open();
        var _lstUnidadMedida = await con.QueryAsync<UnidadMedida>(
            "[trzreceta].[GetListUnidadMedida]",
            new { },
            commandType: CommandType.StoredProcedure
            );

        if (_lstUnidadMedida != null && _lstUnidadMedida.Any())
        {
            responseMaquinaPapelera.Unidades = _lstUnidadMedida.ToList();
        }

        var _lstLineaProduccion = await con.QueryAsync<LineaProduccion>(
            "[trzreceta].[GetListLineaProduccion]",
            new { },
            commandType: CommandType.StoredProcedure
            );

        if (_lstLineaProduccion != null && _lstLineaProduccion.Any())
        {
            responseMaquinaPapelera.LineasProduccion = _lstLineaProduccion.ToList();
        }

        responseMaquinaPapelera.VariablesDisponibles = _context.MaquinasPapeleras.Where(v => v.Estado == true && v.ModoIngreso == false).ToList();

        return responseMaquinaPapelera;
    }
}

public class NewMaquinaPapeleraQueryResponse
{
    public string NombreVariable { get; set; } = "";
    public int LineaProduccion { get; set; }
    public string UnidadMedida { get; set; } = "";
    public decimal ValorMinimo { get; set; } = 0;
    public decimal ValorMaximo { get; set; } = 100;
    public bool ModoIngreso { get; set; }
    public string FormulaCalculo { get; set; } = default!;
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; } = true;
    public List<UnidadMedida>? Unidades { get; set; }
    public List<LineaProduccion>? LineasProduccion { get; set; }
    public List<MaquinaPapelera>? VariablesDisponibles { get; set; }
}

public class NewMaquinaPapeleraQueryProfile : Profile
{
    public NewMaquinaPapeleraQueryProfile() =>
        CreateMap<MaquinaPapelera, NewMaquinaPapeleraQueryResponse>();

}