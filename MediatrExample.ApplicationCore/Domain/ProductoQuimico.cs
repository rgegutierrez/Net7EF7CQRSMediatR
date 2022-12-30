using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain;

[Table("ProductoQuimico", Schema = "trzreceta")]
public class ProductoQuimico
{
    public int ProductoQuimicoId { get; set; }
    public string CodigoSap { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public string Funcion { get; set; } = default!;
    public bool Certificacion { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

