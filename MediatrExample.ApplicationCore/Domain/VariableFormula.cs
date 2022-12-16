using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace MediatrExample.ApplicationCore.Domain;

[Table("VariableFormula", Schema = "trzreceta")]
public class VariableFormula
{
    public int VariableFormulaId { get; set; }
    public string Letra { get; set; } = default!;
    public int? MaquinaPapeleraId { get; set; }
    public MaquinaPapelera MaquinaPapelera { get; set; }
    public int? VariableId { get; set; }
    public MaquinaPapelera Variable { get; set; }
}
