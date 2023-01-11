using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain.Receta;

[Table("RecetaLineaProduccion", Schema = "trzreceta")]
public class RecetaLineaProduccion
{
    public int RecetaLineaProduccionId { get; set; }
    public int RecetaFabricacionId { get; set; }
    public RecetaFabricacion? RecetaFabricacion { get; set; }
    public int LineaProduccionId { get; set; }
    public string LineaProduccionNombre { get; set; } = default!;
    public LineaProduccion? LineaProduccion { get; set; }
}
