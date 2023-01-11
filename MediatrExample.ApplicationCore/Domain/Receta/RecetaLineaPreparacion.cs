using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain.Receta;

[Table("RecetaLineaPreparacion", Schema = "trzreceta")]
public class RecetaLineaPreparacion
{
    public int RecetaLineaPreparacionId { get; set; }
    public int RecetaFabricacionId { get; set; }
    public RecetaFabricacion? RecetaFabricacion { get; set; }
    public int LineaProduccionId { get; set; }
    public string LineaProduccionNombre { get; set; } = default!;
    public LineaProduccion? LineaProduccion { get; set; }
}
