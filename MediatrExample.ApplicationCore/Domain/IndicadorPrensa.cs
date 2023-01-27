using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain;

[Table("IndicadorPrensa", Schema = "trzreceta")]
public class IndicadorPrensa
{
    public int IndicadorPrensaId { get; set; }
    public int? TipoIndicadorPrensaId { get; set; }
    public TipoIndicadorPrensa TipoIndicadorPrensa { get; set; }
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

