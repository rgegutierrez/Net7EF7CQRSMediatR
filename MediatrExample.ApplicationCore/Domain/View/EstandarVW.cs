using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain;

public class EstandarVW
{
    public int EstandarId { get; set; }
    public string ClienteId { get; set; }
    public string ClienteNombre { get; set; }
    public string TipoPapelId { get; set; }
    public string TipoPapelNombre { get; set; }
    public int ValorFisicoPieMaquinaId { get; set; }
    public string ValorFisicoPieMaquinaNombre { get; set; }
    public decimal ValorMinimo { get; set; }
    public decimal ValorPromedio { get; set; }
    public decimal? ValorMaximo { get; set; }
}

