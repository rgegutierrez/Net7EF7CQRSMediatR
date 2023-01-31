﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain;

[Table("TipoIndicadorSecador", Schema = "trzreceta")]
public class TipoIndicadorSecador
{
    public int TipoIndicadorSecadorId { get; set; }
    public string NombreVariable { get; set; } = default!;
}
