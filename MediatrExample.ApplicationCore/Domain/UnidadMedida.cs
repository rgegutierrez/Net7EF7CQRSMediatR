using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain;

[Table("UnidadMedida", Schema = "trzreceta")]
public class UnidadMedida
{
    public int UnidadMedidaId { get; set; }
    public string NombreVariable { get; set; } = default!;
    public string Descripcion { get; set; } = default!;
    public bool Estado { get; set; }
}
