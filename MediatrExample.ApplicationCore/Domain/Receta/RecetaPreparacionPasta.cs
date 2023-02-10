using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain.Receta;

[Table("RecetaPreparacionPasta", Schema = "trzreceta")]
public class RecetaPreparacionPasta
{
    public int RecetaPreparacionPastaId { get; set; }
    public int RecetaLineaPreparacionId { get; set; }
    public RecetaLineaPreparacion RecetaLineaPreparacion { get; set; }
    public int PreparacionPastaId { get; set; }
    public PreparacionPasta PreparacionPasta { get; set; }
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public decimal? Valor { get; set; }
}
