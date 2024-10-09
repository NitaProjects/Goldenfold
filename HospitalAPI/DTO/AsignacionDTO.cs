using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalApi.DTO
{
    public class AsignacionDTO
    {
        public int IdAsignacion { get; set; }
        public int IdPaciente { get; set; }
        public int IdCama { get; set; }
        public string Ubicacion { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime? FechaLiberacion { get; set; }
        public int AsignadoPor { get; set; }
        public string NombrePaciente { get; set; }
        public string NombreUsuario { get; set; }
    }

    public class AsignacionCreateDTO
    {
        [Required(ErrorMessage = "El ID del paciente es obligatorio.")]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "El ID de la cama es obligatorio.")]
        public int IdCama { get; set; }

        [Required(ErrorMessage = "La fecha de asignación es obligatoria.")]
        public DateTime FechaAsignacion { get; set; } = DateTime.Now;

        public DateTime? FechaLiberacion { get; set; }

        [Required(ErrorMessage = "El usuario que asignó la cama es obligatorio.")]
        public int AsignadoPor { get; set; }
    }

    public class AsignacionUpdateDTO
    {
        [Required(ErrorMessage = "El ID de la asignación es obligatorio.")]
        public int IdAsignacion { get; set; }

        [Required(ErrorMessage = "El ID del paciente es obligatorio.")]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "El ID de la cama es obligatorio.")]
        public int IdCama { get; set; }

        [Required(ErrorMessage = "La fecha de asignación es obligatoria.")]
        public DateTime FechaAsignacion { get; set; }

        public DateTime? FechaLiberacion { get; set; }

        [Required(ErrorMessage = "El usuario que asignó la cama es obligatorio.")]
        public int AsignadoPor { get; set; }
    }
}
