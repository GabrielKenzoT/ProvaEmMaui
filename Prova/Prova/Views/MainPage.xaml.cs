using Prova.ViewModels;

namespace Prova.MainPage;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new WeatherViewModel();
    }

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }
}
