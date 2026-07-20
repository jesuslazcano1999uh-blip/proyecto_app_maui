namespace Recordatorios
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Views.RecordatorioDetallePage), typeof(Views.RecordatorioDetallePage));
        }
    }
}
