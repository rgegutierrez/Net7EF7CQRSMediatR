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

namespace MediatrExample.ApplicationCore.Features.MateriaPrimaFeatures.Queries;

public class GetPreparacionPastaQuery : IRequest<GetMateriaPrimaQueryResponse>
{
    public string MateriaPrimaId { get; set; }
}

public class GetMateriaPrimaQueryHandler : IRequestHandler<GetPreparacionPastaQuery, GetMateriaPrimaQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;
    readonly string _connectionString = "";

    public GetMateriaPrimaQueryHandler(MyAppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<GetMateriaPrimaQueryResponse> Handle(GetPreparacionPastaQuery request, CancellationToken cancellationToken)
    {
        var materiaPrima = await _context.MateriasPrimas.FindAsync(request.MateriaPrimaId.FromHashId());

        if (materiaPrima is null)
        {
            throw new NotFoundException(nameof(MateriaPrima), request.MateriaPrimaId);
        }

        var responseMateriaPrima = _mapper.Map<GetMateriaPrimaQueryResponse>(materiaPrima);

        using IDbConnection con = new SqlConnection(_connectionString);
        if (con.State == ConnectionState.Closed) con.Open();
        var _lstUnidadMedida = await con.QueryAsync<UnidadMedida>(
            "[trzreceta].[GetListUnidadMedida]",
            new { },
            commandType: CommandType.StoredProcedure
            );

        if (_lstUnidadMedida != null && _lstUnidadMedida.Any())
        {
            responseMateriaPrima.Unidades = _lstUnidadMedida.ToList();
        }

        return responseMateriaPrima;
    }
}

public class GetMateriaPrimaQueryResponse
{
    public string MateriaPrimaId { get; set; } = default!;
    public string CodigoSap { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
    public List<UnidadMedida>? Unidades { get; set; }
}

public class GetMateriaPrimaQueryProfile : Profile
{
    public GetMateriaPrimaQueryProfile() =>
        CreateMap<MateriaPrima, GetMateriaPrimaQueryResponse>()
            .ForMember(dest =>
                dest.MateriaPrimaId,
                opt => opt.MapFrom(mf => mf.MateriaPrimaId.ToHashId()));

}