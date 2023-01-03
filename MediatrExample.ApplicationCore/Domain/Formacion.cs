using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain;

[Table("Formacion", Schema = "trzreceta")]
public class Formacion
{
    public int FormacionId { get; set; }
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedidaAngulo { get; set; } = default!;
    public decimal RangoAnguloMinimo { get; set; }
    public decimal RangoAnguloMaximo { get; set; }
    public string UnidadMedidaAltura { get; set; } = default!;
    public decimal RangoAlturaMinimo { get; set; }
    public decimal RangoAlturaMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

