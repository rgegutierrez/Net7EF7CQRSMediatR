using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain.Receta;

[Table("RecetaMateriaPrima", Schema = "trzreceta")]
public class RecetaMateriaPrima
{
    public int RecetaMateriaPrimaId { get; set; }
    public int RecetaLineaProduccionId { get; set; }
    public RecetaLineaProduccion RecetaLineaProduccion { get; set; }
    public int MateriaPrimaId { get; set; }
    public MateriaPrima MateriaPrima { get; set; }
    public string CodigoSap { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public decimal Valor { get; set; }
}
