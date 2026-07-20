using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Recordatorios.Models;
using Recordatorios.Services;

namespace Recordatorios.ViewModels
{
    [QueryProperty(nameof(RecordatorioId), "Id")]
    public partial class RecordatorioDetalleViewModel : ObservableObject
    {
        private readonly ApiService _apiService;

        [ObservableProperty]
        private int recordatorioId;

        [ObservableProperty]
        private string titulo = string.Empty;

        [ObservableProperty]
        private string descripcion = string.Empty;

        [ObservableProperty]
        private DateTime fechaRecordatorio = DateTime.Now;

        public RecordatorioDetalleViewModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        partial void OnRecordatorioIdChanged(int value)
        {
            _ = CargarDatosAsync(value);
        }

        private async Task CargarDatosAsync(int id)
        {
            if (id == 0) return;

            try
            {
                var recordatorio = await _apiService.ObtenerRecordatorioPorIdAsync(id);
                if (recordatorio != null)
                {
                    Titulo = recordatorio.Titulo ?? string.Empty;
                    Descripcion = recordatorio.Descripcion ?? string.Empty;
                    FechaRecordatorio = recordatorio.FechaRecordatorio;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo cargar el detalle: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task GuardarAsync()
        {
            if (string.IsNullOrWhiteSpace(Titulo))
            {
                await Shell.Current.DisplayAlert("Aviso", "El título es obligatorio.", "OK");
                return;
            }

            var recordatorio = new Recordatorio
            {
                Id = RecordatorioId,
                Titulo = Titulo,
                Descripcion = Descripcion,
                FechaRecordatorio = FechaRecordatorio
            };

            bool exito = await _apiService.GuardarRecordatorioAsync(recordatorio);
            if (exito)
            {
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "No se pudo guardar el recordatorio en la API.", "OK");
            }
        }
    }
}
