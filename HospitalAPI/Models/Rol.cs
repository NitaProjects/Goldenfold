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
    public int ID_Rol { get; set; }

    [Required]
    [StringLength(50)]
    public RoleType Nombre_Rol { get; set; }

    // Propiedad de navegación
    public ICollection<Usuarios> Usuarios { get; set; }

}