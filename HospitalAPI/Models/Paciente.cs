using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi;
public class Paciente
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int ID_Paciente { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }

    [Required]
    public int Edad { get; set; }

    [Required]
    public DateTime Fecha_Nacimiento { get; set; }

    [StringLength(255)]
    public string Sintomas { get; set; }

    [StringLength(50)]
    public string Estado { get; set; } = "Pendiente de cama";

    [Required]
    public DateTime Fecha_Registro { get; set; } = DateTime.Now;

    [Required]
    [StringLength(50)]
    public string Seguridad_Social { get; set; }

    [StringLength(255)]
    public string Direccion { get; set; }

    [StringLength(20)]
    public string Telefono { get; set; }

    [StringLength(100)]
    public string Email { get; set; }

    public string Historial_Medico { get; set; }

    // Navigation properties
    public ICollection<Asignacion> Asignaciones { get; set; }
    public ICollection<HistorialAlta> HistorialAltas { get; set; }
}



