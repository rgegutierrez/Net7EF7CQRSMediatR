using MediatrExample.ApplicationCore.Domain.Receta;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain;

[Table("RecetaFormacionValor", Schema = "trzreceta")]
public class RecetaFormacionValor
{
    public int RecetaFormacionValorId { get; set; }
    public int RecetaFormacionId { get; set; }
    public RecetaFormacion RecetaFormacion { get; set; }
    public int Foil { get; set; }
    public decimal? ValorAngulo { get; set; }
    public decimal? ValorAltura { get; set; }
}

