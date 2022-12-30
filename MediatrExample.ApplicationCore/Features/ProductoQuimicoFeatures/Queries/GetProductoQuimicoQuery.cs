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

namespace MediatrExample.ApplicationCore.Features.ProductoQuimicoFeatures.Queries;

public class GetProductoQuimicoQuery : IRequest<GetProductoQuimicoQueryResponse>
{
    public string ProductoQuimicoId { get; set; }
}

public class GetProductoQuimicoQueryHandler : IRequestHandler<GetProductoQuimicoQuery, GetProductoQuimicoQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;
    readonly string _connectionString = "";

    public GetProductoQuimicoQueryHandler(MyAppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<GetProductoQuimicoQueryResponse> Handle(GetProductoQuimicoQuery request, CancellationToken cancellationToken)
    {
        var materiaPrima = await _context.ProductosQuimicos.FindAsync(request.ProductoQuimicoId.FromHashId());

        if (materiaPrima is null)
        {
            throw new NotFoundException(nameof(ProductoQuimico), request.ProductoQuimicoId);
        }

        var responseProductoQuimico = _mapper.Map<GetProductoQuimicoQueryResponse>(materiaPrima);

        using IDbConnection con = new SqlConnection(_connectionString);
        if (con.State == ConnectionState.Closed) con.Open();
        var _lstUnidadMedida = await con.QueryAsync<UnidadMedida>(
            "[trzreceta].[GetListUnidadMedida]",
            new { },
            commandType: CommandType.StoredProcedure
            );

        if (_lstUnidadMedida != null && _lstUnidadMedida.Any())
        {
            responseProductoQuimico.Unidades = _lstUnidadMedida.ToList();
        }

        return responseProductoQuimico;
    }
}

public class GetProductoQuimicoQueryResponse
{
    public string ProductoQuimicoId { get; set; } = default!;
    public string CodigoSap { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public string Funcion { get; set; } = default!;
    public bool Certificacion { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
    public List<UnidadMedida>? Unidades { get; set; }
}

public class GetProductoQuimicoQueryProfile : Profile
{
    public GetProductoQuimicoQueryProfile() =>
        CreateMap<ProductoQuimico, GetProductoQuimicoQueryResponse>()
            .ForMember(dest =>
                dest.ProductoQuimicoId,
                opt => opt.MapFrom(mf => mf.ProductoQuimicoId.ToHashId()));

}