namespace MediatrExample.ApplicationCore.Domain.View;

public class RecetaFabricacionVW
{
    public int RecetaFabricacionId { get; set; }
    public int? TipoRecetaId { get; set; }
    public string? TipoRecetaNombre { get; set; }
    public string TipoPapelId { get; set; } = default!;
    public string TipoPapelCodigo { get; set; } = default!;
    public string TipoPapelNombre { get; set; } = default!;
    public string Gramaje { get; set; } = default!;
    public string ClienteId { get; set; } = default!;
    public string ClienteCodigo { get; set; } = default!;
    public string ClienteNombre { get; set; } = default!;
    public int TipoEspecificacionId { get; set; } = default!;
    public string TipoEspecificacionNombre { get; set; } = default!;
    public string TipoEspecificacionDsc { get; set; } = default!;
    public string Tubete { get; set; } = default!;
    public string Diametro { get; set; } = default!;
    public string Tolerancia { get; set; } = default!;
    public int EspecificacionTecnicaId { get; set; }
    public string CodigoReceta { get; set; } = default!;
    public int Version { get; set; }
    public int Estado { get; set; }
    public string? AprobacionGerencia { get; set; }
    public string? AprobacionJefatura { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? InicioVigencia { get; set; }
    public DateTime? TerminoVigencia { get; set; }
}

