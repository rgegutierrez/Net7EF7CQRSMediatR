using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain.Receta;

[Table("RecetaLineaMaquina", Schema = "trzreceta")]
public class RecetaLineaMaquina
{
    public int RecetaLineaMaquinaId { get; set; }
    public int RecetaFabricacionId { get; set; }
    public RecetaFabricacion RecetaFabricacion { get; set; }
    public int LineaProduccionId { get; set; }
    public string LineaProduccionNombre { get; set; } = default!;
    public LineaProduccion LineaProduccion { get; set; }
}
