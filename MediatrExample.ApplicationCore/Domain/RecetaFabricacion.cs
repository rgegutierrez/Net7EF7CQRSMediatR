using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain;

[Table("RecetaFabricacion", Schema = "trzreceta")]

public class RecetaFabricacion
{
    public int RecetaFabricacionId { get; set; }
    public int EspecificacionTecnicaId { get; set; }
    public string CodigoReceta { get; set; } = default!;
    public int Version { get; set; }
    public int Estado { get; set; }
    public bool? AprobacionGerencia { get; set; }
    public bool? AprobacionJefatura { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime InicioVigencia { get; set; }
    public DateTime TerminoVigencia { get; set; }
}

