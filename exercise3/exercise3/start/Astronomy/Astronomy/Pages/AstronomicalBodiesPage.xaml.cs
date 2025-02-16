namespace Astronomy.Pages;

public partial class AstronomicalBodiesPage : ContentPage
{
    public AstronomicalBodiesPage()
    {
        InitializeComponent();
    }

    private async void OnSunButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AstronomicalBodyPage("sun"));
    }
}
