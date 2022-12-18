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

public class GetPreparacionPastaQuery : IRequest<GetPreparacionPastaQueryResponse>
{
    public string PreparacionPastaId { get; set; }
}

public class GetPreparacionPastaQueryHandler : IRequestHandler<GetPreparacionPastaQuery, GetPreparacionPastaQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;
    readonly string _connectionString = "";

    public GetPreparacionPastaQueryHandler(MyAppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<GetPreparacionPastaQueryResponse> Handle(GetPreparacionPastaQuery request, CancellationToken cancellationToken)
    {
        var preparacionPasta = await _context.PreparacionPastas.FindAsync(request.PreparacionPastaId.FromHashId());

        if (preparacionPasta is null)
        {
            throw new NotFoundException(nameof(PreparacionPasta), request.PreparacionPastaId);
        }

        var responsePreparacionPasta = _mapper.Map<GetPreparacionPastaQueryResponse>(preparacionPasta);

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

public class GetPreparacionPastaQueryResponse
{
    public string PreparacionPastaId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
    public List<UnidadMedida>? Unidades { get; set; }
}

public class GetPreparacionPastaQueryProfile : Profile
{
    public GetPreparacionPastaQueryProfile() =>
        CreateMap<PreparacionPasta, GetPreparacionPastaQueryResponse>()
            .ForMember(dest =>
                dest.PreparacionPastaId,
                opt => opt.MapFrom(mf => mf.PreparacionPastaId.ToHashId()));

}