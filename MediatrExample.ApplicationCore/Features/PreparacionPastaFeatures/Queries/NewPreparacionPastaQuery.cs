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

namespace MediatrExample.ApplicationCore.Features.PreparacionPastaFeatures.Queries;

public class NewPreparacionPastaQuery : IRequest<NewPreparacionPastaQueryResponse>
{

}

public class NewPreparacionPastaQueryHandler : IRequestHandler<NewPreparacionPastaQuery, NewPreparacionPastaQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;
    readonly string _connectionString = "";

    public NewPreparacionPastaQueryHandler(MyAppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<NewPreparacionPastaQueryResponse> Handle(NewPreparacionPastaQuery request, CancellationToken cancellationToken)
    {
        var responsePreparacionPasta = new NewPreparacionPastaQueryResponse();

        using IDbConnection con = new SqlConnection(_connectionString);
        if (con.State == ConnectionState.Closed) con.Open();
        var _lstUnidadMedida = await con.QueryAsync<UnidadMedida>(
            "[trzreceta].[GetListUnidadMedida]",
            new { },
            commandType: CommandType.StoredProcedure
            );

        if (_lstUnidadMedida != null && _lstUnidadMedida.Any())
        {
            responsePreparacionPasta.Unidades = _lstUnidadMedida.ToList();
        }

        return responsePreparacionPasta;
    }
}

public class NewPreparacionPastaQueryResponse
{
    public string NombreVariable { get; set; } = "";
    public string UnidadMedida { get; set; } = "";
    public decimal ValorMinimo { get; set; } = 0;
    public decimal ValorMaximo { get; set; } = 100;
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; } = true;
    public List<UnidadMedida>? Unidades { get; set; }
}

public class NewPreparacionPastaQueryProfile : Profile
{
    public NewPreparacionPastaQueryProfile() =>
        CreateMap<PreparacionPasta, NewPreparacionPastaQueryResponse>();

}