using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain;

[Table("LineaProduccion", Schema = "trzreceta")]
public class LineaProduccion
{
    public int LineaProduccionId { get; set; }
    public string NombreVariable { get; set; } = default!;
    public string Descripcion { get; set; } = default!;
    public bool Estado { get; set; }
}
