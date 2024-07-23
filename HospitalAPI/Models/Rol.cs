using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi;

public enum RoleType
{
    Administrativo = 1,
    Medico = 2,
    ControladorCamas = 3,
    AdministrativoSistemas = 4
}
public class Rol
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id_rol { get; set; }

    [Required]
    [StringLength(50)]
    public RoleType nombre_rol { get; set; }

    // Propiedad de navegación
    public ICollection<Usuario> usuarios { get; set; }

}