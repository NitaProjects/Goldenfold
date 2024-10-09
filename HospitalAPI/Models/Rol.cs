using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi
{
    public class Rol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_rol")]
        public int IdRol { get; set; }

        [Column("nombre_rol")]
        public string NombreRol { get; set; }

        // Propiedad de navegación
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
