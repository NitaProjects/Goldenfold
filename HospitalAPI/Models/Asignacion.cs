using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi
{
    public class Asignacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id_asignacion")]
        public int IdAsignacion { get; set; }

        [ForeignKey("Paciente")]
        [Column("id_paciente")]
        public int IdPaciente { get; set; }

        [ForeignKey("Cama")]
        [Column("id_cama")]
        public int IdCama { get; set; }

        [Column("fecha_asignacion")]
        public DateTime FechaAsignacion { get; set; } = DateTime.Now;

        [Column("fecha_liberacion")]
        public DateTime? FechaLiberacion { get; set; }

        [ForeignKey("Usuario")]
        [Column("asignado_por")]
        public int AsignadoPor { get; set; }
        
        public Paciente Paciente { get; set; }
        public Cama Cama { get; set; } 
        public Usuario Usuario { get; set; }
        public ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();

        // Constructor
        public Asignacion()
        {
            Ingresos = new List<Ingreso>();
        }

    }
}

