using Microsoft.Extensions.Logging;
using Recordatorios.Services;
using Recordatorios.ViewModels;
using Recordatorios.Views;

namespace Recordatorios
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Registro de Servicios y Cliente HTTP
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<ApiService>();

            // Registro de ViewModels
            builder.Services.AddTransient<RecordatoriosViewModel>();
            builder.Services.AddTransient<RecordatorioDetalleViewModel>();

            // Registro de Vistas
            builder.Services.AddTransient<RecordatoriosPage>();
            builder.Services.AddTransient<RecordatorioDetallePage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
