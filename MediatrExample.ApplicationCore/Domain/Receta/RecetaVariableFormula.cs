using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain.Receta;

[Table("RecetaVariableFormula", Schema = "trzreceta")]
public class RecetaVariableFormula
{
    public int RecetaVariableFormulaId { get; set; }
    public string Letra { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public int? RecetaMaquinaPapeleraId { get; set; }
    public RecetaMaquinaPapelera RecetaMaquinaPapelera { get; set; }
}
