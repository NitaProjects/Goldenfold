using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi;

public class Habitaciones
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID_Habitacion { get; set; }

    [Required]
    [StringLength(2)]
    public string Edificio { get; set; }

    [Required]
    [StringLength(2)]
    public string Planta { get; set; }

    [Required]
    [StringLength(2)]
    public string Habitacion { get; set; }
}