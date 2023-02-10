using MediatrExample.ApplicationCore.Domain.Receta;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain;

[Table("RecetaTiroMaquina", Schema = "trzreceta")]
public class RecetaTiroMaquina
{
    public int RecetaTiroMaquinaId { get; set; }
    public int RecetaFabricacionId { get; set; }
    public RecetaFabricacion RecetaFabricacion { get; set; }
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public decimal? Valor { get; set; }
    public string? Comentario { get; set; } = default!;
}

