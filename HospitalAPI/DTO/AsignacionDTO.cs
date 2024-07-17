namespace HospitalAPI.DTO
{
    public class AsignacionDTO
    {
        public int ID_Asignacion { get; set; }
        public int ID_Paciente { get; set; }
        public string Ubicacion { get; set; }
        public DateTime Fecha_Asignacion { get; set; }
        public DateTime? Fecha_Liberacion { get; set; }
        public int Asignado_Por { get; set; }
    }
}
