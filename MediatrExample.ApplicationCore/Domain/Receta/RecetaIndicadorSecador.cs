using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain.Receta;

[Table("RecetaIndicadorSecador", Schema = "trzreceta")]
public class RecetaIndicadorSecador
{
    public int RecetaIndicadorSecadorId { get; set; }
    public int RecetaTipoIndicadorSecadorId { get; set; }
    public RecetaTipoIndicadorSecador RecetaTipoIndicadorSecador { get; set; }
    public int IndicadorSecadorId { get; set; }
    public IndicadorSecador IndicadorSecador { get; set; }
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public decimal? Valor { get; set; }
}
