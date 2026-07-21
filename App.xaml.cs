using Recordatorios.Views;

namespace Recordatorios;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Inicia mostrando la pantalla de Login
        MainPage = new LoginPage();
    }
}
