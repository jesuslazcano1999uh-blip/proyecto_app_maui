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
        private readonly IServiceProvider _serviceProvider;

        [ObservableProperty]
        private bool estaCargando;

        public ObservableCollection<Recordatorio> Recordatorios { get; } = new();

        public RecordatoriosViewModel(ApiService apiService, IServiceProvider serviceProvider)
        {
            _apiService = apiService;
            _serviceProvider = serviceProvider;
        }

        [RelayCommand]
        public async Task CargarRecordatoriosAsync()
        {
            if (EstaCargando) return;

            try
            {
                EstaCargando = true;

                if (_apiService != null)
                {
                    var lista = await _apiService.ObtenerRecordatoriosAsync();
                    Recordatorios.Clear();

                    if (lista != null)
                    {
                        foreach (var item in lista)
                        {
                            Recordatorios.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (Application.Current?.MainPage != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", $"No se pudieron cargar los datos: {ex.Message}", "OK");
                }
            }
            finally
            {
                EstaCargando = false;
            }
        }

        [RelayCommand]
        private async Task IrANuevoAsync()
        {
            if (Application.Current?.MainPage?.Navigation != null && _serviceProvider != null)
            {
                var detallePage = _serviceProvider.GetService<RecordatorioDetallePage>();
                await Application.Current.MainPage.Navigation.PushAsync(detallePage);
            }
        }

        [RelayCommand]
        private async Task IrADetalleAsync(Recordatorio recordatorio)
        {
            if (recordatorio == null) return;

            if (Application.Current?.MainPage?.Navigation != null && _serviceProvider != null)
            {
                var detallePage = _serviceProvider.GetService<RecordatorioDetallePage>();
                await Application.Current.MainPage.Navigation.PushAsync(detallePage);
            }
        }

        [RelayCommand]
        private async Task EliminarAsync(Recordatorio recordatorio)
        {
            if (recordatorio == null) return;

            bool confirmar = false;
            if (Application.Current?.MainPage != null)
            {
                confirmar = await Application.Current.MainPage.DisplayAlert("Confirmar", $"¿Deseas eliminar el recordatorio '{recordatorio.Titulo}'?", "Sí", "No");
            }

            if (confirmar)
            {
                bool exito = await _apiService.EliminarRecordatorioAsync(recordatorio.Id);
                if (exito)
                {
                    Recordatorios.Remove(recordatorio);
                }
                else
                {
                    if (Application.Current?.MainPage != null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "No se pudo eliminar el registro en la API.", "OK");
                    }
                }
            }
        }
    }
}