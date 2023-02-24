using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain.Receta;

[Table("RecetaTipoIndicadorVacio", Schema = "trzreceta")]
public class RecetaTipoIndicadorVacio
{
    public int RecetaTipoIndicadorVacioId { get; set; }
    public int RecetaFabricacionId { get; set; }
    public RecetaFabricacion? RecetaFabricacion { get; set; }
    public int TipoIndicadorVacioId { get; set; }
    public string NombreVariable { get; set; } = default!;
    public TipoIndicadorVacio? TipoIndicadorVacio { get; set; }
    public string? UnidadMedida { get; set; } = default!;
    public int? Orden { get; set; }
    public bool? MostrarUnidad { get; set; }
    public int? Factor { get; set; }
}
