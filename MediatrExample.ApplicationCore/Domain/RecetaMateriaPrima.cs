using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain;

[Table("RecetaMateriaPrima", Schema = "trzreceta")]
public class RecetaMateriaPrima
{
    public int RecetaMateriaPrimaId { get; set; }
    public int RecetaFabricacionId { get; set; }
    public RecetaFabricacion RecetaFabricacion { get; set; }
    public int LineaProduccionId { get; set; }
    public string LineaProduccionNombre { get; set; } = default!;
    public LineaProduccion LineaProduccion { get; set; }
    public int MateriaPrimaId { get; set; }
    public string CodigoSap { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public decimal Valor { get; set; }
}
