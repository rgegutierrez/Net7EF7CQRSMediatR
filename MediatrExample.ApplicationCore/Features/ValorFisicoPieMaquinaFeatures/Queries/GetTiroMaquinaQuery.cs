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

namespace MediatrExample.ApplicationCore.Features.ValorFisicoPieMaquinaFeatures.Queries;

public class GetValorFisicoPieMaquinaQuery : IRequest<GetValorFisicoPieMaquinaQueryResponse>
{
    public string ValorFisicoPieMaquinaId { get; set; }
}

public class GetValorFisicoPieMaquinaQueryHandler : IRequestHandler<GetValorFisicoPieMaquinaQuery, GetValorFisicoPieMaquinaQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;
    readonly string _connectionString = "";

    public GetValorFisicoPieMaquinaQueryHandler(MyAppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<GetValorFisicoPieMaquinaQueryResponse> Handle(GetValorFisicoPieMaquinaQuery request, CancellationToken cancellationToken)
    {
        var materiaPrima = await _context.ValoresFisicoPieMaquina.FindAsync(request.ValorFisicoPieMaquinaId.FromHashId());

        if (materiaPrima is null)
        {
            throw new NotFoundException(nameof(ValorFisicoPieMaquina), request.ValorFisicoPieMaquinaId);
        }

        var responseValorFisicoPieMaquina = _mapper.Map<GetValorFisicoPieMaquinaQueryResponse>(materiaPrima);

        using IDbConnection con = new SqlConnection(_connectionString);
        if (con.State == ConnectionState.Closed) con.Open();
        var _lstUnidadMedida = await con.QueryAsync<UnidadMedida>(
            "[trzreceta].[GetListUnidadMedida]",
            new { },
            commandType: CommandType.StoredProcedure
            );

        if (_lstUnidadMedida != null && _lstUnidadMedida.Any())
        {
            responseValorFisicoPieMaquina.Unidades = _lstUnidadMedida.ToList();
        }

        return responseValorFisicoPieMaquina;
    }
}

public class GetValorFisicoPieMaquinaQueryResponse
{
    public string ValorFisicoPieMaquinaId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
    public List<UnidadMedida>? Unidades { get; set; }
}

public class GetValorFisicoPieMaquinaQueryProfile : Profile
{
    public GetValorFisicoPieMaquinaQueryProfile() =>
        CreateMap<ValorFisicoPieMaquina, GetValorFisicoPieMaquinaQueryResponse>()
            .ForMember(dest =>
                dest.ValorFisicoPieMaquinaId,
                opt => opt.MapFrom(mf => mf.ValorFisicoPieMaquinaId.ToHashId()));

}