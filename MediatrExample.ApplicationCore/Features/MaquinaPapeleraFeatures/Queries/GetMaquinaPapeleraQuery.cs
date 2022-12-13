using AutoMapper;
using Azure;
using MediatR;
using MediatrExample.ApplicationCore.Common.Exceptions;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;

namespace MediatrExample.ApplicationCore.Features.MaquinaPapeleraFeatures.Queries;

public class GetMaquinaPapeleraQuery : IRequest<GetMaquinaPapeleraQueryResponse>
{
    public string MaquinaPapeleraId { get; set; }
}

public class GetMaquinaPapeleraQueryHandler : IRequestHandler<GetMaquinaPapeleraQuery, GetMaquinaPapeleraQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;
    readonly string _connectionString = "";

    public GetMaquinaPapeleraQueryHandler(MyAppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<GetMaquinaPapeleraQueryResponse> Handle(GetMaquinaPapeleraQuery request, CancellationToken cancellationToken)
    {
        var materiaPrima = await _context.MaquinasPapeleras.FindAsync(request.MaquinaPapeleraId.FromHashId());

        if (materiaPrima is null)
        {
            throw new NotFoundException(nameof(MaquinaPapelera), request.MaquinaPapeleraId);
        }

        var responseMaquinaPapelera = _mapper.Map<GetMaquinaPapeleraQueryResponse>(materiaPrima);

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

        responseMaquinaPapelera.Variables = _context.MaquinasPapeleras.Where(v => v.Estado == true).ToList();

        return responseMaquinaPapelera;
    }
}

public class GetMaquinaPapeleraQueryResponse
{
    public string MaquinaPapeleraId { get; set; } = default!;
    public int Orden { get; set; }
    public string NombreVariable { get; set; } = default!;
    public int LineaProduccion { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public int ValorMinimo { get; set; }
    public int ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool ModoIngreso { get; set; }
    public string FormulaCalculo { get; set; } = default!;
    public bool Estado { get; set; }
    public List<UnidadMedida>? Unidades { get; set; }
    public List<LineaProduccion>? LineasProduccion { get; set; }
    public List<MaquinaPapelera>? Variables { get; set; }
}

public class GetMaquinaPapeleraQueryProfile : Profile
{
    public GetMaquinaPapeleraQueryProfile() =>
        CreateMap<MaquinaPapelera, GetMaquinaPapeleraQueryResponse>()
            .ForMember(dest =>
                dest.MaquinaPapeleraId,
                opt => opt.MapFrom(mf => mf.MaquinaPapeleraId.ToHashId()));

}