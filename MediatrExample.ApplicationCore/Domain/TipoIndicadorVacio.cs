using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain;

[Table("TipoIndicadorVacio", Schema = "trzreceta")]
public class TipoIndicadorVacio
{
    public int TipoIndicadorVacioId { get; set; }
    public string NombreVariable { get; set; } = default!;
    public string? UnidadMedida { get; set; } = default!;
    public int? Orden { get; set; }
    public bool? MostrarUnidad { get; set; }
    public int? Factor { get; set; }
}

