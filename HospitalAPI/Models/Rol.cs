using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi;
public class Rol
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ID_Rol { get; set; }

    [Required]
    [StringLength(50)]
    public string Nombre_Rol { get; set; }

    // Propiedad de navegación
    public ICollection<Usuarios> Usuarios { get; set; }

}