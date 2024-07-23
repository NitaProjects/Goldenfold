using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi;

public class Asignacion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id_asignacion { get; set; }

    [ForeignKey("Paciente")]
    public int id_paciente { get; set; }

    [ForeignKey("Cama")]
    [MaxLength(10)]
    public string ubicacion { get; set; }

    [Required]
    public DateTime fecha_asignacion { get; set; } = DateTime.Now;

    public DateTime? fecha_liberacion { get; set; }

    [ForeignKey("Usuario")]
    public int asignado_por { get; set; }

    // Propiedades de navegación
    public Paciente paciente { get; set; }
    public Cama cama { get; set; }
    public Usuario usuario { get; set; }

}