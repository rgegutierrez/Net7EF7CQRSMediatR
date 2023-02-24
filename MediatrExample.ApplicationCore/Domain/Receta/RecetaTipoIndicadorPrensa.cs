using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain.Receta;

[Table("RecetaTipoIndicadorPrensa", Schema = "trzreceta")]
public class RecetaTipoIndicadorPrensa
{
    public int RecetaTipoIndicadorPrensaId { get; set; }
    public int RecetaFabricacionId { get; set; }
    public RecetaFabricacion? RecetaFabricacion { get; set; }
    public int TipoIndicadorPrensaId { get; set; }
    public string NombreVariable { get; set; } = default!;
    public TipoIndicadorPrensa? TipoIndicadorPrensa { get; set; }
    public string? UnidadMedida { get; set; } = default!;
    public int? Orden { get; set; }
    public bool? MostrarUnidad { get; set; }
    public int? Factor { get; set; }
}
