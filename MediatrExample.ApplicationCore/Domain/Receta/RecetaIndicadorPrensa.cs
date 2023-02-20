using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain.Receta;

[Table("RecetaIndicadorPrensa", Schema = "trzreceta")]
public class RecetaIndicadorPrensa
{
    public int RecetaIndicadorPrensaId { get; set; }
    public int RecetaTipoIndicadorPrensaId { get; set; }
    public RecetaTipoIndicadorPrensa RecetaTipoIndicadorPrensa { get; set; }
    public int IndicadorPrensaId { get; set; }
    public IndicadorPrensa IndicadorPrensa { get; set; }
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public decimal? Valor { get; set; }
}
