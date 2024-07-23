namespace HospitalApi.DTO
{
    public class HistorialAltasDTO
    {
        public int id_historial { get; set; }
        public int id_paciente { get; set; }
        public DateTime fecha_alta { get; set; }
        public string diagnostico { get; set; }
        public string tratamiento { get; set; }
    }
}
