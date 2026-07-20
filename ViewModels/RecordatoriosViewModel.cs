using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Recordatorios.Models;
using Recordatorios.Services;
using Recordatorios.Views;

namespace Recordatorios.ViewModels
{
    public partial class RecordatoriosViewModel : ObservableObject
    {
        private readonly ApiService _apiService;

        [ObservableProperty]
        private bool estaCargando;

        public ObservableCollection<Recordatorio> Recordatorios { get; } = new();

        public RecordatoriosViewModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [RelayCommand]
        public async Task CargarRecordatoriosAsync()
        {
            if (EstaCargando) return;

            try
            {
                EstaCargando = true;
                var lista = await _apiService.ObtenerRecordatoriosAsync();
                Recordatorios.Clear();
                foreach (var item in lista)
                {
                    Recordatorios.Add(item);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudieron cargar los datos: {ex.Message}", "OK");
            }
            finally
            {
                EstaCargando = false;
            }
        }

        [RelayCommand]
        private async Task IrANuevoAsync()
        {
            await Shell.Current.GoToAsync(nameof(RecordatorioDetallePage));
        }

        [RelayCommand]
        private async Task IrADetalleAsync(Recordatorio recordatorio)
        {
            if (recordatorio == null) return;
            await Shell.Current.GoToAsync($"{nameof(RecordatorioDetallePage)}?Id={recordatorio.Id}");
        }

        [RelayCommand]
        private async Task EliminarAsync(Recordatorio recordatorio)
        {
            if (recordatorio == null) return;

            bool confirmar = await Shell.Current.DisplayAlert("Confirmar", $"¿Deseas eliminar el recordatorio '{recordatorio.Titulo}'?", "Sí", "No");
            if (confirmar)
            {
                bool exito = await _apiService.EliminarRecordatorioAsync(recordatorio.Id);
                if (exito)
                {
                    Recordatorios.Remove(recordatorio);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "No se pudo eliminar el registro en la API.", "OK");
                }
            }
        }
    }
}
