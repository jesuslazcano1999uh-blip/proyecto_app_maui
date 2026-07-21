using Recordatorios.Services;
using Recordatorios.ViewModels;
using Recordatorios.Views;

namespace Recordatorios.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string usuario = UserEntry.Text;
        string password = PasswordEntry.Text;

        if (usuario == "admin" && password == "1234")
        {
            // Obtenemos el contenedor de servicios actual de la app de forma segura
            var serviceProvider = Handler?.MauiContext?.Services ?? Application.Current?.Handler?.MauiContext?.Services;

            var httpClient = new HttpClient();
            var apiService = new ApiService(httpClient);

            // Le pasamos ambos parámetros que ahora exige el ViewModel
            var viewModel = new RecordatoriosViewModel(apiService, serviceProvider);
            var recordatoriosPage = new RecordatoriosPage(viewModel);

            Application.Current.MainPage = new NavigationPage(recordatoriosPage);
        }
        else
        {
            await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
        }
    }
}