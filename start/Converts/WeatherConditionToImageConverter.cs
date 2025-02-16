﻿using System.Globalization;
using WeatherClient.Models;

namespace WeatherClient.Converters;

internal class WeatherConditionToImageConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is WeatherType weatherCondition)
        {
            return weatherCondition switch
            {
                WeatherType.Sunny => ImageSource.FromFile("sunny.png"),
                WeatherType.Cloudy => ImageSource.FromFile("cloud.png"),
                WeatherType.Rainy => ImageSource.FromFile("rain.png"),
                WeatherType.Snowy => ImageSource.FromFile("snow.png"),
                _ => ImageSource.FromFile("question.png") // Para valores desconocidos
            };
        }

        return ImageSource.FromFile("question.png"); // Manejo de error
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();
}
