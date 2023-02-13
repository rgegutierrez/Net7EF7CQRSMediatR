﻿using MediatrExample.ApplicationCore.Domain.Receta;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediatrExample.ApplicationCore.Domain;

[Table("RecetaFormacion", Schema = "trzreceta")]
public class RecetaFormacion
{
    public int RecetaFormacionId { get; set; }
    public int RecetaFabricacionId { get; set; }
    public RecetaFabricacion RecetaFabricacion { get; set; }
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedidaAngulo { get; set; } = default!;
    public decimal RangoAnguloMinimo { get; set; }
    public decimal RangoAnguloMaximo { get; set; }
    public string UnidadMedidaAltura { get; set; } = default!;
    public decimal RangoAlturaMinimo { get; set; }
    public decimal RangoAlturaMaximo { get; set; }
    public bool Obligatoria { get; set; }
}

