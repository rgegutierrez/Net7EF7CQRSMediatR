using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using MediatrExample.ApplicationCore.Domain.Receta;

namespace MediatrExample.ApplicationCore.Domain;

[Table("RecetaVariableFormula", Schema = "trzreceta")]
public class RecetaVariableFormula
{
    public int RecetaVariableFormulaId { get; set; }
    public string Letra { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public int? RecetaMaquinaPapeleraId { get; set; }
    public RecetaMaquinaPapelera RecetaMaquinaPapelera { get; set; }
}
