using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi;

public class Cama
{
    [Key]
    [StringLength(10)]
    public string Ubicacion { get; set; }

    [StringLength(50)]
    public string Estado { get; set; } = "Disponible";

    [StringLength(50)]
    public string Tipo { get; set; }

    // Navigation properties
    public ICollection<Asignacion> Asignaciones { get; set; }
}