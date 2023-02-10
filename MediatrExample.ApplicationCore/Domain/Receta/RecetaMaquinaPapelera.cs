using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain.Receta;

[Table("RecetaMaquinaPapelera", Schema = "trzreceta")]
public class RecetaMaquinaPapelera
{
    public int RecetaMaquinaPapeleraId { get; set; }
    public int RecetaLineaMaquinaId { get; set; }
    public RecetaLineaMaquina RecetaLineaMaquina { get; set; }
    public int MaquinaPapeleraId { get; set; }
    public MaquinaPapelera MaquinaPapelera { get; set; }
    public int Orden { get; set; }
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal? ValorMinimo { get; set; }
    public decimal? ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool ModoIngreso { get; set; }
    public string FormulaCalculo { get; set; } = default!;
    public decimal? Valor { get; set; }
    public string? Comentario { get; set; } = default!;
}
