using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi;
public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdUsuario { get; set; }

    [Required]
    [StringLength(100)]
    public string Nombre { get; set; }

    [Required]
    [StringLength(50)]
    public string NombreUsuario { get; set; }

    [Required]
    [StringLength(255)]
    public string Contrasenya { get; set; }

    [ForeignKey("Rol")]
    public int IdRol { get; set; }

    // Propiedades de navegación
    public Rol Rol { get; set; }
    public ICollection<Asignacion> Asignaciones { get; set; }

}