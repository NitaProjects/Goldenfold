using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalApi
{
    public class Paciente
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("id_paciente")]
        public int IdPaciente { get; set; }

        [Column("nombre")] 
        public string Nombre { get; set; }

        [Column("dni")]
        public string Dni { get; set; }

        [Column("fecha_nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Column("estado")]
        public EstadoPaciente Estado { get; set; } = EstadoPaciente.Registrado;

        [Column("fecha_registro")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Column("seguridad_social")]
        public string SeguridadSocial { get; set; }

        [Column("direccion")]
        public string Direccion { get; set; }

        [Column("telefono")]
        public string Telefono { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("historial_medico")]
        public string HistorialMedico { get; set; }

        // Propiedades de navegación
        public ICollection<HistorialAlta> HistorialAltas { get; set; }
        public ICollection<Asignacion> Asignaciones { get; set; }
        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
        public ICollection<Ingreso> Ingresos { get; set; } = new List<Ingreso>();

        // Constructor
        public Paciente()
        {
            FechaRegistro = DateTime.Now;
            HistorialAltas = new List<HistorialAlta>();
            Asignaciones = new List<Asignacion>();
            Consultas = new List<Consulta>();
            Ingresos = new List<Ingreso>();
        }
    }

    // Enum para el estado del paciente
    public enum EstadoPaciente
    {
        Registrado,
        EnConsulta,
        Ingresado,
        Alta
    }
}
