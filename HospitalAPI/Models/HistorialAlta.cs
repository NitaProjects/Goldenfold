using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi;

public class HistorialAlta
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID_Historial { get; set; }

    [ForeignKey("Paciente")]
    public int ID_Paciente { get; set; }
    public Paciente Paciente { get; set; }

    [Required]
    public DateTime Fecha_Alta { get; set; }

    [StringLength(255)]
    public string Diagnostico { get; set; }

    [StringLength(255)]
    public string Tratamiento { get; set; }
}