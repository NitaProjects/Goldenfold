using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi;

public class HistorialAlta
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id_historial { get; set; }

    [ForeignKey("Paciente")]
    public int id_paciente { get; set; }

    [Required]
    public DateTime fecha_alta { get; set; } = DateTime.Now;

    [StringLength(255)]
    public string diagnostico { get; set; }

    [StringLength(255)]
    public string tratamiento { get; set; }

    // Propiedad de navegación
    public Paciente paciente { get; set; }

}