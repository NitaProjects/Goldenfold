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
    public int IdPaciente { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }

    [Required]

    [Range(0, int.MaxValue, ErrorMessage = "La edad debe ser un numero positivo.")]
    public int Edad { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    [StringLength(255)]
    public string Sintomas { get; set; }

    [StringLength(50)]
    public EstadoPaciente Estado { get; set; } = EstadoPaciente.PendienteDeCama;

    [Required]
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    [Required]
    [StringLength(12, MinimumLength = 12)]
    public string SeguridadSocial { get; set; }

    [StringLength(255)]
    public string Direccion { get; set; }

    [StringLength(20)]
    [DataType(DataType.PhoneNumber)]
    public string Telefono { get; set; }

    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; }

    public string HistorialMedico { get; set; }

    // Propiedades de navegaci�n
    public ICollection<HistorialAlta> HistorialAltas { get; set; }
    public ICollection<Asignacion> Asignaciones { get; set; }
}



