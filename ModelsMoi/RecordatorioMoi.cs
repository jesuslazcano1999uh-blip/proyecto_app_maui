/// <summary>
/// Clase que representa un recordatorio dentro de la aplicación.
/// Almacena la información principal y calcula automáticamente
/// el color de prioridad de acuerdo con la fecha y hora establecidas.
/// </summary>
/// <remarks>
/// Autor: Moi
/// Materia: Diseño y Desarrollo de Aplicaciones Móviles
/// Proyecto: Aplicación de Recordatorios
/// Fecha: Julio 2026
/// </remarks>
public class Recordatorio
{
    /// <summary>
    /// Identificador único del recordatorio.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Título del recordatorio.
    /// </summary>
    public string Titulo { get; set; } = string.Empty;

    /// <summary>
    /// Descripción o información adicional del recordatorio.
    /// </summary>
    public string? Descripcion { get; set; }

    /// <summary>
    /// Fecha y hora en la que se debe mostrar el recordatorio.
    /// </summary>
    public DateTime FechaHora { get; set; }

    /// <summary>
    /// Calcula el color de prioridad del recordatorio según el tiempo
    /// restante para la fecha programada.
    /// </summary>
    public string ColorPrioridad
    {
        get
        {
            var horasRestantes = (FechaHora - DateTime.Now).TotalHours;

            // Prioridad alta: menos de una hora.
            if (horasRestantes <= 1 && horasRestantes > 0)
                return "Red";

            // Prioridad media-alta: entre 1 y 6 horas.
            else if (horasRestantes <= 6 && horasRestantes > 1)
                return "Orange";

            // Prioridad media: entre 6 y 24 horas.
            else if (horasRestantes <= 24 && horasRestantes > 6)
                return "Blue";

            // Prioridad baja: más de 24 horas.
            else if (horasRestantes > 24)
                return "Green";

            // El recordatorio ya venció.
            else
                return "Gray";
        }
    }
}
