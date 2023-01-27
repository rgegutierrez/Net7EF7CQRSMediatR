using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain;

[Table("IndicadorSecador", Schema = "trzreceta")]
public class IndicadorSecador
{
    public int IndicadorSecadorId { get; set; }
    public int? TipoIndicadorSecadorId { get; set; }
    public TipoIndicadorSecador TipoIndicadorSecador { get; set; }
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

