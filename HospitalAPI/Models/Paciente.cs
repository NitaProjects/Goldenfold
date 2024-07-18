using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi;

public enum EstadoPaciente
{
    PendienteDeCama,
    EnCama
}
public class Paciente
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public int ID_Paciente { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "La edad debe ser un número positivo.")]
    public int Edad { get; set; }

    public DateTime? Fecha_Nacimiento { get; set; }

    [StringLength(255)]
    public string Sintomas { get; set; }

    [StringLength(50)]
    public EstadoPaciente Estado { get; set; } = EstadoPaciente.PendienteDeCama;

    [Required]
    public DateTime Fecha_Registro { get; set; } = DateTime.Now;

    [Required]
    [StringLength(12, MinimumLength = 12)]
    public string Seguridad_Social { get; set; }

    [StringLength(255)]
    public string Direccion { get; set; }

    [StringLength(20)]
    public string Telefono { get; set; }

    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    public string Historial_Medico { get; set; }

    // Propiedades de navegación
    public ICollection<HistorialAlta> HistorialAltas { get; set; }
    public ICollection<Asignacion> Asignaciones { get; set; }

}



