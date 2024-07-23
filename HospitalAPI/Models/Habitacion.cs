using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi;

public class Habitacion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id_habitacion { get; set; }

    [Required]
    [StringLength(2)]
    public string edificio { get; set; }

    [Required]
    [StringLength(2)]
    public string planta { get; set; }

    [Required]
    [StringLength(2)]
    public string habitacion { get; set; }
}