using Recordatorios.ViewModels;

namespace Recordatorios.Views
{
    public partial class RecordatoriosPage : ContentPage
    {
        private readonly RecordatoriosViewModel _viewModel;

        public RecordatoriosPage(RecordatoriosViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.CargarRecordatoriosCommand.ExecuteAsync(null);
        }
    }
}