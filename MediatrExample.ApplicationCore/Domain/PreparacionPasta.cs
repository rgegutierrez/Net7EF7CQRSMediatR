using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain;

[Table("PreparacionPasta", Schema = "trzreceta")]
public class PreparacionPasta
{
    public int PreparacionPastaId { get; set; }
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

