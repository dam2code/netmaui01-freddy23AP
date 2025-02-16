using Microsoft.Maui.Controls;
using MovieCatalog.ViewModels;

namespace MovieCatalog.Views;

public partial class MovieDetailPage : ContentPage
{
    public MovieDetailPage(MovieViewModel selectedMovie)  // Asegurar que reciba el parámetro
    {
        InitializeComponent();
        BindingContext = selectedMovie;
    }
}
