using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalApi.DTO
{
    public class PacienteDTO
    {
        public int IdPaciente { get; set; }

        public string Nombre { get; set; }

        public string Dni { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public string SeguridadSocial { get; set; }

        public string? Direccion { get; set; }

        public string? Telefono { get; set; }

        public string? Email { get; set; }

        public string? HistorialMedico { get; set; }

        public EstadoPaciente Estado { get; set; }

        public DateTime FechaRegistro { get; set; }
    }

    public class PacienteCreateDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [RegularExpression(@"^[0-9]{8}[A-Za-z]$", ErrorMessage = "El DNI debe tener 8 números seguidos de una letra.")]
        public string Dni { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El número de seguridad social es obligatorio.")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "El número de la seguridad social debe tener exactamente 12 dígitos.")]
        public string SeguridadSocial { get; set; }

        [StringLength(255, ErrorMessage = "La dirección no puede tener más de 255 caracteres.")]
        public string? Direccion { get; set; }

        [RegularExpression(@"^\d{9}$", ErrorMessage = "El teléfono debe contener exactamente 9 dígitos.")]
        public string? Telefono { get; set; }

        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [StringLength(100, ErrorMessage = "El correo electrónico no puede tener más de 100 caracteres.")]
        public string? Email { get; set; }

        public string? HistorialMedico { get; set; }

        public EstadoPaciente Estado { get; set; } = EstadoPaciente.Registrado;
    }

    public class PacienteUpdateDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio.")]
        [RegularExpression(@"^[0-9]{8}[A-Za-z]$", ErrorMessage = "El DNI debe tener 8 números seguidos de una letra.")]
        public string Dni { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El número de seguridad social es obligatorio.")]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "El número de la seguridad social debe tener exactamente 12 dígitos.")]
        public string SeguridadSocial { get; set; }

        [StringLength(255, ErrorMessage = "La dirección no puede tener más de 255 caracteres.")]
        public string? Direccion { get; set; }

        [RegularExpression(@"^\d{9}$", ErrorMessage = "El teléfono debe contener exactamente 9 dígitos.")]
        public string? Telefono { get; set; }

        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [StringLength(100, ErrorMessage = "El correo electrónico no puede tener más de 100 caracteres.")]
        public string? Email { get; set; }

        public string? HistorialMedico { get; set; }

        public EstadoPaciente Estado { get; set; }
    }
}
