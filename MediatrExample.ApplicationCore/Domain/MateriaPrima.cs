using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain;

[Table("MateriaPrima", Schema = "trzreceta")]
public class MateriaPrima
{
    public int MateriaPrimaId { get; set; }
    public string CodigoSap { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

