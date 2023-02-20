using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain;

[Table("TipoIndicadorPrensa", Schema = "trzreceta")]
public class TipoIndicadorPrensa
{
    public int TipoIndicadorPrensaId { get; set; }
    public string NombreVariable { get; set; } = default!;
    public string? UnidadMedida { get; set; } = default!;
    public int? Orden { get; set; }
    public bool? MostrarUnidad { get; set; }
}

