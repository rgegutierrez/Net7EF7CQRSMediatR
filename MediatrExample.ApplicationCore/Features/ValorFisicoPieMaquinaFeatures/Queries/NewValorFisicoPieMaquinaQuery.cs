﻿using AutoMapper;
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

public class NewValorFisicoPieMaquinaQuery : IRequest<NewValorFisicoPieMaquinaQueryResponse>
{

}

public class NewValorFisicoPieMaquinaQueryHandler : IRequestHandler<NewValorFisicoPieMaquinaQuery, NewValorFisicoPieMaquinaQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;
    readonly string _connectionString = "";

    public NewValorFisicoPieMaquinaQueryHandler(MyAppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("Default");
    }
    public async Task<NewValorFisicoPieMaquinaQueryResponse> Handle(NewValorFisicoPieMaquinaQuery request, CancellationToken cancellationToken)
    {
        var responseValorFisicoPieMaquina = new NewValorFisicoPieMaquinaQueryResponse();

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

public class NewValorFisicoPieMaquinaQueryResponse
{
    public string NombreVariable { get; set; } = "";
    public string UnidadMedida { get; set; } = "";
    public decimal ValorMinimo { get; set; } = 0;
    public decimal ValorMaximo { get; set; } = 100;
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; } = true;
    public List<UnidadMedida>? Unidades { get; set; }
}

public class NewValorFisicoPieMaquinaQueryProfile : Profile
{
    public NewValorFisicoPieMaquinaQueryProfile() =>
        CreateMap<ValorFisicoPieMaquina, NewValorFisicoPieMaquinaQueryResponse>();

}