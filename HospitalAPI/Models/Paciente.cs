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
    public int id_paciente { get; set; }

    [Required]
    [StringLength(100)]
    public string nombre { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "La edad debe ser un número positivo.")]
    public int edad { get; set; }

    public DateTime? fecha_nacimiento { get; set; }

    [StringLength(255)]
    public string sintomas { get; set; }

    [StringLength(50)]
    public EstadoPaciente estado { get; set; } = EstadoPaciente.PendienteDeCama;

    [Required]
    public DateTime fecha_registro { get; set; } = DateTime.Now;

    [Required]
    [StringLength(12, MinimumLength = 12)]
    public string seguridad_social { get; set; }

    [StringLength(255)]
    public string direccion { get; set; }

    [StringLength(20)]
    [DataType(DataType.PhoneNumber)]
    public string telefono { get; set; }

    [StringLength(100)]
    [EmailAddress]
    public string email { get; set; }

    public string historial_medico { get; set; }

    // Propiedades de navegación
    public ICollection<HistorialAlta> historial_altas { get; set; }
    public ICollection<Asignacion> asignaciones { get; set; }

}



