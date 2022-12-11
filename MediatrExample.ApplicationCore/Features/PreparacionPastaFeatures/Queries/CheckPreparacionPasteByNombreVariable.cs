using AutoMapper;
using Dapper;
using MediatR;
using MediatrExample.ApplicationCore.Common.Exceptions;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using MediatrExample.ApplicationCore.Infrastructure.Persistence.Migrations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MediatrExample.ApplicationCore.Features.PreparacionPastaFeatures.Queries;

public class CheckPreparacionPasteByNombreVariable : IRequest<CheckPreparacionPasteByNombreVariableResponse>
{
    public string NombreVariable { get; set; }
    public string? PreparacionPastaId { get; set; }
}

public class CheckPreparacionPasteByNombreVariableHandler : IRequestHandler<CheckPreparacionPasteByNombreVariable, CheckPreparacionPasteByNombreVariableResponse>
{
    private readonly MyAppDbContext _context;

    public CheckPreparacionPasteByNombreVariableHandler(MyAppDbContext context)
    {
        _context = context;
    }
    public async Task<CheckPreparacionPasteByNombreVariableResponse> Handle(CheckPreparacionPasteByNombreVariable request, CancellationToken cancellationToken)
    {
        var response = new CheckPreparacionPasteByNombreVariableResponse();

        if(request.PreparacionPastaId is null)
        {
            var preparacionPasta = await _context.PreparacionPastas.Where(
                v => v.NombreVariable == request.NombreVariable
                ).FirstOrDefaultAsync();

            if (preparacionPasta is null)
            {
                response.Check = true;
            }
        } else
        {
            var preparacionPasta = await _context.PreparacionPastas.Where(
                v => v.NombreVariable == request.NombreVariable && 
                v.PreparacionPastaId != request.PreparacionPastaId.FromHashId()
                ).FirstOrDefaultAsync();

            if (preparacionPasta is null)
            {
                response.Check = true;
            }
        }

        return response;
    }
}

public class CheckPreparacionPasteByNombreVariableResponse
{
    public bool Check { get; set; } = false;
}