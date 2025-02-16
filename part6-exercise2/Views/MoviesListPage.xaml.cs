using Microsoft.Maui.Controls;  // Asegurar que ContentPage y Navigation estén disponibles
using MovieCatalog.ViewModels;   // Para MovieViewModel
using System.Windows.Input;

namespace MovieCatalog.Views;

public partial class MoviesListPage : ContentPage
{
    public MoviesListPage()
    {
        InitializeComponent();
        BindingContext = App.MainViewModel;  // Asegurar el contexto de datos
    }

    private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is MovieViewModel selectedMovie)
        {
            await Navigation.PushAsync(new MovieDetailPage(selectedMovie)); // Pasar la película seleccionada
        }
    }
}
