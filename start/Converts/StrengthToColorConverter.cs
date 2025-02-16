using System.Globalization;
using Microsoft.Maui.Controls;
using WeatherClient.Models;

namespace WeatherClient.Converters;

public class StrengthToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Strength strength)
        {
            return strength switch
            {
                Strength.Weak => Colors.OrangeRed,
                Strength.Good => Colors.Yellow,
                Strength.Strong => Colors.LightBlue,
                _ => Colors.LightGray
            };
        }
        return Colors.LightGray;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
