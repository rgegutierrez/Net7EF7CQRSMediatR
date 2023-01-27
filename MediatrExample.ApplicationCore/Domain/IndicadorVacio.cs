using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain;

[Table("IndicadorVacio", Schema = "trzreceta")]
public class IndicadorVacio
{
    public int IndicadorVacioId { get; set; }
    public int? TipoIndicadorVacioId { get; set; }
    public TipoIndicadorVacio TipoIndicadorVacio { get; set; }
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

