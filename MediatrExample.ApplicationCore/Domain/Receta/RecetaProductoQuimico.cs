using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain.Receta;

[Table("RecetaProductoQuimico", Schema = "trzreceta")]
public class RecetaProductoQuimico
{
    public int RecetaProductoQuimicoId { get; set; }
    public int RecetaFabricacionId { get; set; }
    public RecetaFabricacion RecetaFabricacion { get; set; }
    public string CodigoSap { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public string Funcion { get; set; } = default!;
    public bool Certificacion { get; set; }
    public bool Obligatoria { get; set; }
    public decimal? Valor { get; set; }
}
