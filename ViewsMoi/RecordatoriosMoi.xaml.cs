using Recordatorios.ViewModels;

namespace Recordatorios.ViewsMoi;

public partial class RecordatoriosPage : ContentPage
{
    public RecordatoriosPage(RecordatoriosViewModel viewModel)
    {
        BindingContext = viewModel;
    }
}