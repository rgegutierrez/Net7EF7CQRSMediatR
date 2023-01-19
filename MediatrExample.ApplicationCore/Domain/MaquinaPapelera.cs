using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain;

[Table("MaquinaPapelera", Schema = "trzreceta")]
public class MaquinaPapelera
{
    public int MaquinaPapeleraId { get; set; }
    public int Orden { get; set; }
    public string NombreVariable { get; set; } = default!;
    public int LineaProduccion { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public decimal? ValorMinimo { get; set; }
    public decimal? ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool ModoIngreso { get; set; }
    public string FormulaCalculo { get; set; } = default!;
    public bool Estado { get; set; }

    [InverseProperty(nameof(VariableFormula.MaquinaPapelera))]
    public IList<VariableFormula> MaquinasPapeleras { get; set; } = new List<VariableFormula>();

    [InverseProperty(nameof(VariableFormula.Variable))]
    public IList<VariableFormula> Variables { get; set; } = new List<VariableFormula>();
}

