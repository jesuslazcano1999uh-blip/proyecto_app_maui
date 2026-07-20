using Recordatorios.ViewModels;

namespace Recordatorios.Views
{
    public partial class RecordatorioDetallePage : ContentPage
    {
        public RecordatorioDetallePage(RecordatorioDetalleViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}