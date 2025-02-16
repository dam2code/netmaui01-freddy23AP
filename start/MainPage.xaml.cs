using System.ComponentModel;
using WeatherClient.Models;

namespace WeatherClient;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    private Strength _passwordStrength;
    public Strength PasswordStrength
    {
        get => _passwordStrength;
        set
        {
            _passwordStrength = value;
            OnPropertyChanged(nameof(PasswordStrength));
        }
    }

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this; // Asignamos el BindingContext a esta clase
    }

    public decimal BillAmount { get; set; }

    private async void OnRefreshClicked(object sender, EventArgs e)
    {
        string postalCode = PostalCodeEntry.Text;

        // Simulando un cambio en la condición del clima
        var random = new Random();
        var weatherTypes = Enum.GetValues(typeof(WeatherType));
        WeatherType newCondition = (WeatherType)weatherTypes.GetValue(random.Next(weatherTypes.Length));

        WeatherData data = new WeatherData
        {
            Condition = newCondition,
            Temperature = random.Next(30, 100),
            Humidity = random.Next(10, 100),
            Precipitation = random.Next(0, 100),
            Wind = random.Next(0, 50)
        };

        // Asigna los nuevos datos al BindingContext
        BindingContext = data;
    }


    private void PasswordEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        int length = e.NewTextValue.Length;
        if (length < 4)
            PasswordStrength = Strength.Weak;
        else if (length < 8)
            PasswordStrength = Strength.Good;
        else
            PasswordStrength = Strength.Strong;
    }
}
