namespace HospitalApi.DTO
{
    public class HistorialAltaDTO
    {
        public int IdHistorial { get; set; }
        public int IdPaciente { get; set; }
        public DateTime FechaAlta { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
    }
}
