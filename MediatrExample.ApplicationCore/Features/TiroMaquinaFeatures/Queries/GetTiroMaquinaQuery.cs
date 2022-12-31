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

namespace MediatrExample.ApplicationCore.Features.TiroMaquinaFeatures.Queries;

public class GetTiroMaquinaQuery : IRequest<GetTiroMaquinaQueryResponse>
{
    public string TiroMaquinaId { get; set; }
}

public class GetTiroMaquinaQueryHandler : IRequestHandler<GetTiroMaquinaQuery, GetTiroMaquinaQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;
    readonly string _connectionString = "";

    public GetTiroMaquinaQueryHandler(MyAppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<GetTiroMaquinaQueryResponse> Handle(GetTiroMaquinaQuery request, CancellationToken cancellationToken)
    {
        var materiaPrima = await _context.TirosMaquina.FindAsync(request.TiroMaquinaId.FromHashId());

        if (materiaPrima is null)
        {
            throw new NotFoundException(nameof(TiroMaquina), request.TiroMaquinaId);
        }

        var responseTiroMaquina = _mapper.Map<GetTiroMaquinaQueryResponse>(materiaPrima);

        using IDbConnection con = new SqlConnection(_connectionString);
        if (con.State == ConnectionState.Closed) con.Open();
        var _lstUnidadMedida = await con.QueryAsync<UnidadMedida>(
            "[trzreceta].[GetListUnidadMedida]",
            new { },
            commandType: CommandType.StoredProcedure
            );

        if (_lstUnidadMedida != null && _lstUnidadMedida.Any())
        {
            responseTiroMaquina.Unidades = _lstUnidadMedida.ToList();
        }

        return responseTiroMaquina;
    }
}

public class GetTiroMaquinaQueryResponse
{
    public string TiroMaquinaId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
    public List<UnidadMedida>? Unidades { get; set; }
}

public class GetTiroMaquinaQueryProfile : Profile
{
    public GetTiroMaquinaQueryProfile() =>
        CreateMap<TiroMaquina, GetTiroMaquinaQueryResponse>()
            .ForMember(dest =>
                dest.TiroMaquinaId,
                opt => opt.MapFrom(mf => mf.TiroMaquinaId.ToHashId()));

}