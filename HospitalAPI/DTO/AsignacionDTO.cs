namespace HospitalApi.DTO
{
    public class AsignacionDTO
    {
        public int IdAsignacion { get; set; }
        public int IdPaciente { get; set; }
        public string Ubicacion { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime? FechaLiberacion { get; set; }
        public int AsignadoPor { get; set; }
    }
}
