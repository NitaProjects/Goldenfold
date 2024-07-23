using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi;

public class Asignacion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdAsignacion { get; set; }

    [ForeignKey("Paciente")]
    public int IdPaciente { get; set; }

    [ForeignKey("Cama")]
    [MaxLength(10)]
    public string Ubicacion { get; set; }

    [Required]
    public DateTime FechaAsignacion { get; set; } = DateTime.Now;

    public DateTime? FechaLiberacion { get; set; }

    [ForeignKey("Usuario")]
    public int AsignadoPor { get; set; }

    // Propiedades de navegación
    public Paciente Paciente { get; set; }
    public Cama Cama { get; set; }
    public Usuario Usuario { get; set; }

}