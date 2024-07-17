namespace HospitalAPI.DTO
{
    public class HistorialAltaDTO
    {
        public int ID_Historial { get; set; }
        public int ID_Paciente { get; set; }
        public DateTime Fecha_Alta { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
    }
}
