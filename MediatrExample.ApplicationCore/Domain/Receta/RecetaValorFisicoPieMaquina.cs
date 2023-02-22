using MediatrExample.ApplicationCore.Domain.Receta;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain;

[Table("RecetaValorFisicoPieMaquina", Schema = "trzreceta")]
public class RecetaValorFisicoPieMaquina
{
    public int RecetaValorFisicoPieMaquinaId { get; set; }
    public int RecetaFabricacionId { get; set; }
    public RecetaFabricacion RecetaFabricacion { get; set; }
    public int ValorFisicoPieMaquinaId { get; set; }
    public ValorFisicoPieMaquina ValorFisicoPieMaquina { get; set; }
    public string NombreVariable { get; set; } = default!;
    public string? UnidadMedida { get; set; } = default!;
    public decimal? ValorMinimo { get; set; }
    public decimal? ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public decimal? ValorMin { get; set; }
    public decimal? ValorEst { get; set; }
    public decimal? ValorMax { get; set; }
}

