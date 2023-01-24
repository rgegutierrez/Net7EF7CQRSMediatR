using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Domain.Receta;
using MediatrExample.ApplicationCore.Features.RecetaFabricacionFeatures.Queries;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MediatrExample.ApplicationCore.Features.RecetaFabricacionFeatures.Commands;
public class CheckRecetaFabricacionCommand : IRequest
{

}

public class CheckRecetaFabricacionCommandHandler : IRequestHandler<CheckRecetaFabricacionCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;
    readonly string _connectionString = "";

    public CheckRecetaFabricacionCommandHandler(MyAppDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _connectionString = configuration.GetConnectionString("Default");
    }

    public async Task<Unit> Handle(CheckRecetaFabricacionCommand request, CancellationToken cancellationToken)
    {
        string url = "http://localhost:8080/#/trazabilidad/receta_fabricacion/crear/";
        string displayCaduca = "display:none";
        string bodyCaduca = "";
        string displayAviso = "display:none";
        string bodyAviso = "";

        string FilePathUnidadReceta = Directory.GetCurrentDirectory() + "\\Templates\\UnidadReceta.html";
        StreamReader strUnidadReceta = new(FilePathUnidadReceta);
        string TemplateUnidadReceta = strUnidadReceta.ReadToEnd();
        strUnidadReceta.Close();

        var dateNow = DateTime.Now;
        var lstRecetasVigentes = _context.Recetas
            .Where(o => o.Estado == 1 && o.Notificacion == 0)
            .ToListAsync()
            .Result;

        List<RecetaFabricacion> lstRecetasCaducadas = new();
        List<RecetaFabricacion> lstRecetasAviso = new();

        foreach (var item in lstRecetasVigentes)
        {
            if (item.TerminoVigencia is null) continue;

            var itemReceta = _context.RecetasVW
                .Where(o => o.RecetaFabricacionId == item.RecetaFabricacionId)
                .ProjectTo<GetListRecetaFabricacionVWQueryResponse>(_mapper.ConfigurationProvider)
                .First();
            DateTime inicio = (DateTime)itemReceta.InicioVigencia;
            DateTime termino = (DateTime)itemReceta.TerminoVigencia;

            var dateVigencia = item.TerminoVigencia;
            TimeSpan difference = (TimeSpan)(dateVigencia - dateNow);

            if (difference.Days <= 0)
            {
                item.Estado = 3;
                item.Notificacion = 1;
                
                var aux = TemplateUnidadReceta;
                aux = aux
                    .Replace("[color]", "#ff0000")
                    .Replace("[url]", string.Format("{0}{1}", url, itemReceta.RecetaFabricacionId))
                    .Replace("[codigoReceta]", itemReceta.CodigoReceta)
                    .Replace("[fechaInicio]", inicio.ToString("dd-MM-yyyy"))
                    .Replace("[fechaTermino]", termino.ToString("dd-MM-yyyy"))
                    .Replace("[cliente]", itemReceta.ClienteNombre);
                bodyCaduca = string.Format("{0}{1}", bodyCaduca, aux);
                displayCaduca = "";
                lstRecetasCaducadas.Add(item);
            } else if(difference.Days <= 30)
            {
                item.Notificacion = 1;

                var aux = TemplateUnidadReceta;
                aux = aux
                    .Replace("[color]", "#05a419")
                    .Replace("[url]", string.Format("{0}{1}", url, itemReceta.RecetaFabricacionId))
                    .Replace("[codigoReceta]", itemReceta.CodigoReceta)
                    .Replace("[fechaInicio]", inicio.ToString("dd-MM-yyyy"))
                    .Replace("[fechaTermino]", termino.ToString("dd-MM-yyyy"))
                    .Replace("[cliente]", itemReceta.ClienteNombre);
                bodyAviso = string.Format("{0}{1}", bodyAviso, aux);
                displayAviso = "";
                lstRecetasAviso.Add(item);
            }
        }

        if(lstRecetasCaducadas.Any())
        {
            string Title = String.Format("[Trazabilidad] Recetas de Fabricación caducadas ({0})", dateNow.ToString("dd-MM-yyyy"));
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\EstadoRecetaFabricacion.html";
            StreamReader str = new(FilePath);
            string Template = str.ReadToEnd();
            Template = Template
                .Replace("[displayCaduca]", displayCaduca)
                .Replace("[bodyCaduca]", bodyCaduca)
                .Replace("[displayAviso]", "display:none")
                .Replace("[bodyAviso]", "");
            str.Close();

            using IDbConnection con = new SqlConnection(_connectionString);
            if (con.State == ConnectionState.Closed) con.Open();
            var _lstUnidadMedida = await con.QueryAsync<string>(
                "[trzreceta].[SendEmail]",
                new
                {
                    PT_Email = "jgutierrez@fpc.cl;varanguiz@fpc.cl",
                    PT_Title = Title,
                    PT_Template = Template
                },
                commandType: CommandType.StoredProcedure
                );
        }

        if (lstRecetasAviso.Any())
        {
            string Title = String.Format("[Trazabilidad] Recetas de Fabricación por cadudar ({0})", dateNow.ToString("dd-MM-yyyy"));
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\EstadoRecetaFabricacion.html";
            StreamReader str = new(FilePath);
            string Template = str.ReadToEnd();
            Template = Template
                .Replace("[displayCaduca]", "display:none")
                .Replace("[bodyCaduca]", "")
                .Replace("[displayAviso]", displayAviso)
                .Replace("[bodyAviso]", bodyAviso);
            str.Close();

            using IDbConnection con = new SqlConnection(_connectionString);
            if (con.State == ConnectionState.Closed) con.Open();
            var _lstUnidadMedida = await con.QueryAsync<string>(
                "[trzreceta].[SendEmail]",
                new
                {
                    PT_Email = "jgutierrez@fpc.cl",
                    PT_Title = Title,
                    PT_Template = Template
                },
                commandType: CommandType.StoredProcedure
                );
        }

        //await _context.SaveChangesAsync();

        return Unit.Value;
    }
}