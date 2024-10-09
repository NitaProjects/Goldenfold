using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi
{
    public class HistorialAlta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_historial")]
        public int IdHistorial { get; set; }

        [ForeignKey("Paciente")]
        [Column("id_paciente")]
        public int IdPaciente { get; set; }

        [Column("fecha_alta")]
        public DateTime FechaAlta { get; set; } = DateTime.Now;

        [Column("diagnostico")]
        public string Diagnostico { get; set; }

        [Column("tratamiento")]
        public string Tratamiento { get; set; }
        
        [ForeignKey("Medico")]
        [Column("id_medico")]
        public int IdMedico { get; set; }
        public Usuario Medico { get; set; }
        public virtual Paciente Paciente { get; set; }
    }
}
