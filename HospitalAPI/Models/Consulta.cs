using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi
{
    public class Consulta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("id_consulta")]
        public int IdConsulta { get; set; }

        [ForeignKey("Paciente")]
        [Column("id_paciente")]
        public int? IdPaciente { get; set; }

        [ForeignKey("Medico")]
        [Column("id_medico")]
        public int? IdMedico { get; set; }

        [Column("motivo")]
        public string Motivo { get; set; }

        [Column("fecha_solicitud")]
        public DateTime FechaSolicitud { get; set; } = DateTime.Now;

        [Column("fecha_consulta")]
        public DateTime? FechaConsulta { get; set; }

        [Column("estado")]
        public EstadoConsulta Estado { get; set; } = EstadoConsulta.pendiente;
        // Propiedades de navegación
        public virtual Paciente Paciente { get; set; }
        public virtual Usuario Medico { get; set; }
    }
    public enum EstadoConsulta
    {
        pendiente,
        completada
    }
}
