using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi;

public class Asignacion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID_Asignacion { get; set; }

    [ForeignKey("Paciente")]
    public int ID_Paciente { get; set; }

    [ForeignKey("Cama")]
    [MaxLength(10)]
    public string Ubicacion { get; set; }

    [Required]
    public DateTime Fecha_Asignacion { get; set; } = DateTime.Now;

    public DateTime? Fecha_Liberacion { get; set; }

    [ForeignKey("Usuario")]
    public int Asignado_Por { get; set; }

    // Propiedades de navegación
    public Paciente Paciente { get; set; }
    public Cama Cama { get; set; }
    public Usuarios Usuario { get; set; }

}