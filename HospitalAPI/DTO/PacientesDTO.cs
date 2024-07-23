namespace HospitalApi.DTO
{
    public class PacientesDTO
    {
        public int id_paciente { get; set; }
        public string nombre { get; set; }
        public int edad { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string sintomas { get; set; }
        public EstadoPaciente estado { get; set; }
        public DateTime fecha_registro { get; set; }
        public string seguridad_social { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string historial_medico { get; set; }
    }
}
