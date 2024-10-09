using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi
{
    public class Ingreso
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("id_ingreso")]
        public int IdIngreso { get; set; }

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

        [Column("fecha_ingreso")]
        public DateTime? FechaIngreso { get; set; }

        [Column("estado")]
        public EstadoIngreso Estado { get; set; } = EstadoIngreso.Pendiente;

        [ForeignKey("Asignacion")] 
        [Column("id_asignacion")]
        public int? IdAsignacion { get; set; }

        [Column("tipo_cama")]
        public TipoCama TipoCama { get; set; } = TipoCama.General;
        // Propiedades de navegación
        public virtual Paciente Paciente { get; set; }
        public virtual Usuario Medico { get; set; }
        public virtual Asignacion Asignacion { get; set; }
    }
    // Enum para el estado del ingreso
    public enum EstadoIngreso
    {
        Pendiente,
        Ingresado,
        Rechazado,
        Alta
    }
}
