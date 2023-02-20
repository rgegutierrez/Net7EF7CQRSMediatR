using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain.Receta;

[Table("RecetaTipoIndicadorSecador", Schema = "trzreceta")]
public class RecetaTipoIndicadorSecador
{
    public int RecetaTipoIndicadorSecadorId { get; set; }
    public int RecetaFabricacionId { get; set; }
    public RecetaFabricacion? RecetaFabricacion { get; set; }
    public int TipoIndicadorSecadorId { get; set; }
    public string NombreVariable { get; set; } = default!;
    public TipoIndicadorSecador? TipoIndicadorSecador { get; set; }
    public string? UnidadMedida { get; set; } = default!;
    public int? Orden { get; set; }
    public bool? MostrarUnidad { get; set; }
}
