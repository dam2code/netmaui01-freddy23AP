namespace Astronomy.Pages;

public partial class AstronomicalBodyPage : ContentPage
{
    public AstronomicalBodyPage(string astroName)
    {
        InitializeComponent();
        UpdateAstroBodyUI(astroName);
    }

    void UpdateAstroBodyUI(string astroName)
    {
        AstronomicalBody body = FindAstroData(astroName);

        Title = body.Name;
        lblIcon.Text = body.EmojiIcon;
        lblName.Text = body.Name;
        lblMass.Text = $"Mass: {body.Mass}";
        lblCircumference.Text = $"Circumference: {body.Circumference}";
        lblAge.Text = $"Age: {body.Age}";
    }

    AstronomicalBody FindAstroData(string astronomicalBodyName)
    {
        return astronomicalBodyName switch
        {
            "comet" => SolarSystemData.HalleysComet,
            "earth" => SolarSystemData.Earth,
            "moon" => SolarSystemData.Moon,
            "sun" => SolarSystemData.Sun,
            _ => throw new ArgumentException("Invalid astronomical body")
        };
    }
}
