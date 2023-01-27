using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain;

[Table("Estandar", Schema = "trzreceta")]
public class Estandar
{
    public int EstandarId { get; set; }
    public int ClienteId { get; set; }
    public int TipoPapelId { get; set; }
    public int ValorFisicoPieMaquinaId { get; set; }
    public ValorFisicoPieMaquina ValorFisicoPieMaquina { get; set; }
    public decimal ValorMinimo { get; set; }
    public decimal ValorPromedio { get; set; }
    public decimal ValorMaximo { get; set; }
}

