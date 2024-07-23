using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi;

public class HistorialAlta
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdHistorial { get; set; }

    [ForeignKey("Paciente")]
    public int IdPaciente { get; set; }

    [Required]
    public DateTime FechaAlta { get; set; } = DateTime.Now;

    [StringLength(255)]
    public string Diagnostico { get; set; }

    [StringLength(255)]
    public string Tratamiento { get; set; }

    // Propiedad de navegación
    public Paciente Paciente { get; set; }

}