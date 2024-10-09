using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("nombre_usuario")]
        public string NombreUsuario { get; set; }


        [Column("contrasenya")]
        public string Contrasenya { get; set; }

        [ForeignKey("Rol")]
        [Column("id_rol")]
        public int IdRol { get; set; }

        // Relación con HistorialAlta
        public ICollection<HistorialAlta> HistorialAltas { get; set; }

        // Propiedades de navegación
        public virtual Rol Rol { get; set; }
        public ICollection<Asignacion> Asignaciones { get; set; } = new List<Asignacion>();
        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
        public ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();

        public Usuario()
        {
            Consultas = new List<Consulta>();
            Ingresos = new List<Ingreso>();
        }
    }
}
