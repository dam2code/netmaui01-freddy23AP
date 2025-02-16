namespace MovieCatalog.Views;

public partial class MoviesListPage : ContentPage
{
    public MoviesListPage()
    {
        InitializeComponent(); 
        BindingContext = App.MainViewModel;
    }

    private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        await Navigation.PushAsync(new Views.MovieDetailPage());
    }


    private void MenuItem_Clicked(object sender, EventArgs e)
    {
        if (sender is MenuItem menuItem && menuItem.BindingContext is ViewModels.MovieViewModel movie)
        {
            App.MainViewModel?.DeleteMovie(movie);
        }
    }
}
