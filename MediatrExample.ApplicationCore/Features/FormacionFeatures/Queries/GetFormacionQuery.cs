using AutoMapper;
using Dapper;
using MediatR;
using MediatrExample.ApplicationCore.Common.Exceptions;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MediatrExample.ApplicationCore.Features.FormacionFeatures.Queries;

public class GetFormacionQuery : IRequest<GetFormacionQueryResponse>
{
    public string FormacionId { get; set; }
}

public class GetFormacionQueryHandler : IRequestHandler<GetFormacionQuery, GetFormacionQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;
    readonly string _connectionString = "";

    public GetFormacionQueryHandler(MyAppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<GetFormacionQueryResponse> Handle(GetFormacionQuery request, CancellationToken cancellationToken)
    {
        var obj = await _context.Formaciones.FindAsync(request.FormacionId.FromHashId());

        if (obj is null)
        {
            throw new NotFoundException(nameof(Formacion), request.FormacionId);
        }

        var responseFormacion = _mapper.Map<GetFormacionQueryResponse>(obj);

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

public class GetFormacionQueryResponse
{
    public string FormacionId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedidaAngulo { get; set; } = default!;
    public decimal RangoAnguloMinimo { get; set; }
    public decimal RangoAnguloMaximo { get; set; }
    public string UnidadMedidaAltura { get; set; } = default!;
    public decimal RangoAlturaMinimo { get; set; }
    public decimal RangoAlturaMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
    public List<UnidadMedida>? Unidades { get; set; }
}

public class GetFormacionQueryProfile : Profile
{
    public GetFormacionQueryProfile() =>
        CreateMap<Formacion, GetFormacionQueryResponse>()
            .ForMember(dest =>
                dest.FormacionId,
                opt => opt.MapFrom(mf => mf.FormacionId.ToHashId()));

}