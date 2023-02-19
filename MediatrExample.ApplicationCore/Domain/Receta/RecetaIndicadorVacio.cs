using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain.Receta;

[Table("RecetaIndicadorVacio", Schema = "trzreceta")]
public class RecetaIndicadorVacio
{
    public int RecetaIndicadorVacioId { get; set; }
    public int RecetaTipoIndicadorVacioId { get; set; }
    public RecetaTipoIndicadorVacio RecetaTipoIndicadorVacio { get; set; }
    public int IndicadorVacioId { get; set; }
    public IndicadorVacio IndicadorVacio { get; set; }
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public decimal? Valor { get; set; }
}
