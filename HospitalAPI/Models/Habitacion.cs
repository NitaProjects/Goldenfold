using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi;

public class Habitacion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdHabitacion { get; set; }

    [Required]
    [StringLength(2)]
    public string Edificio { get; set; }

    [Required]
    [StringLength(2)]
    public string Planta { get; set; }

    [Required]
    [StringLength(2)]
    public int NumeroHabitacion { get; set; }
}