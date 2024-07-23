using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi;
public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int id_usuario { get; set; }

    [Required]
    [StringLength(100)]
    public string nombre { get; set; }

    [Required]
    [StringLength(50)]
    public string usuario { get; set; }

    [Required]
    [StringLength(255)]
    public string contrasenya { get; set; }

    [ForeignKey("Rol")]
    public int id_rol { get; set; }

    // Propiedades de navegación
    public Rol rol { get; set; }
    public ICollection<Asignacion> asignaciones { get; set; }

}