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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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
        var obj = await _context.MaquinasPapeleras.FindAsync(request.MaquinaPapeleraId.FromHashId());

        if (obj is null)
        {
            throw new NotFoundException(nameof(MaquinaPapelera), request.MaquinaPapeleraId);
        }

        //_context.Entry(obj).Collection(p => p.Variables).Load();
        //obj.Variables = _context.VariablesFormula.Where(o => o.MaquinaPapeleraId == obj.MaquinaPapeleraId).ToList();
        var responseMaquinaPapelera = _mapper.Map<GetMaquinaPapeleraQueryResponse>(obj);

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

        responseMaquinaPapelera.LineasProduccion = _context.LineasProduccion.Where(
            v => v.Estado == true
            ).ToList();

        responseMaquinaPapelera.VariablesDisponibles = _context.MaquinasPapeleras.Where(
            v => v.Estado == true && v.ModoIngreso == false
            ).ToList();

        responseMaquinaPapelera.Variables = _context.VariablesFormula.Where(
            o => o.MaquinaPapeleraId == obj.MaquinaPapeleraId
            ).ToList();

        var existsVariableAsignada = _context.VariablesFormula.Where(
            o => o.VariableId == obj.MaquinaPapeleraId
            ).ToList();

        if (existsVariableAsignada.Count() > 0)
        {
            responseMaquinaPapelera.EstadoReadonly = true;
        }

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
    public decimal? ValorMinimo { get; set; }
    public decimal? ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool ModoIngreso { get; set; }
    public string FormulaCalculo { get; set; } = default!;
    public bool Estado { get; set; }
    public List<UnidadMedida>? Unidades { get; set; }
    public List<LineaProduccion>? LineasProduccion { get; set; }
    public List<MaquinaPapelera>? VariablesDisponibles { get; set; }
    public List<VariableFormula>? Variables { get; set; }
    public bool EstadoReadonly { get; set; } = false;
}

public class GetMaquinaPapeleraQueryProfile : Profile
{
    public GetMaquinaPapeleraQueryProfile() =>
        CreateMap<MaquinaPapelera, GetMaquinaPapeleraQueryResponse>()
            .ForMember(dest =>
                dest.MaquinaPapeleraId,
                opt => opt.MapFrom(mf => mf.MaquinaPapeleraId.ToHashId()));

}