using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Recordatorios.Models;

namespace Recordatorios.ViewModels;

/// <summary>
/// ViewModel encargado de administrar la información y las operaciones
/// relacionadas con los recordatorios de la aplicación.
/// Implementa el patrón MVVM para separar la lógica de negocio de la interfaz.
/// </summary>
/// <remarks>
/// Autor: Moi
/// Materia: Diseño y Desarrollo de Aplicaciones Móviles
/// Proyecto: Aplicación de Recordatorios
/// Fecha: Julio 2026
/// </remarks>
public partial class RecordatoriosViewModel : ObservableObject
{
    /// <summary>
    /// Colección observable que almacena los recordatorios
    /// mostrados en la interfaz de usuario.
    /// </summary>
    public ObservableCollection<Recordatorio> Recordatorioss { get; } = new();

    /// <summary>
    /// Indica si la aplicación se encuentra cargando información.
    /// </summary>
    [ObservableProperty]
    private bool estaaCargando;

    /// <summary>
    /// Mensaje que se muestra cuando no existen recordatorios registrados.
    /// </summary>
    [ObservableProperty]
    private string mensajeVacio = "No hay recordatorios. ¡Agrega uno!";

    /// <summary>
    /// Constructor de la clase.
    /// Inicializa el ViewModel y carga los recordatorios.
    /// </summary>
    public RecordatoriosViewModel()
    {
        _ = CargarRecordatoriossAsync();
    }

    /// <summary>
    /// Carga los recordatorios disponibles.
    /// Actualmente utiliza datos de prueba para simular
    /// la información obtenida desde una API o base de datos.
    /// </summary>
    [RelayCommand]
    private async Task CargarRecordatoriossAsync()
    {
        estaaCargando = true;

        try
        {
            // Datos de prueba.
            var lista = new List<Recordatorio>
            {
                new Recordatorio
                {
                    Id = 1,
                    Titulo = "Reunión con equipo",
                    Descripcion = "Proyecto final",
                    FechaHora = DateTime.Now.AddHours(2)
                },
                new Recordatorio
                {
                    Id = 2,
                    Titulo = "Entregar reporte",
                    Descripcion = "PDF",
                    FechaHora = DateTime.Now.AddDays(1)
                },
                new Recordatorio
                {
                    Id = 3,
                    Titulo = "Llamada con cliente",
                    Descripcion = "Confirmar",
                    FechaHora = DateTime.Now.AddHours(5)
                }
            };

            // Ordena los recordatorios por fecha y hora.
            var ordenada = lista.OrderBy(r => r.FechaHora).ToList();

            Recordatorioss.Clear();

            foreach (var r in ordenada)
            {
                Recordatorioss.Add(r);
            }
        }
        catch (Exception ex)
        {
            // Muestra un mensaje si ocurre algún error.
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            estaaCargando = false;
        }
    }

    /// <summary>
    /// Muestra un mensaje indicando que la función para crear
    /// un nuevo recordatorio aún se encuentra en desarrollo.
    /// </summary>
    [RelayCommand]
    private async Task IrANuevooAsync()
    {
        await Shell.Current.DisplayAlert(
            "Información",
            "Función de crear recordatorio (en desarrollo).",
            "Aceptar");
    }
}