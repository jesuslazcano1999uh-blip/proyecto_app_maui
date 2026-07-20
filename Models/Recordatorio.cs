namespace Recordatorios.Models
{
    public class Recordatorio
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaRecordatorio { get; set; } = DateTime.Now;
    }
}
