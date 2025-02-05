namespace Phoneword;

public partial class MainPage : ContentPage
{
    string translatedNumber;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnTranslate(object sender, EventArgs e)
    {
        string enteredNumber = PhoneNumberText.Text;

        if (string.IsNullOrWhiteSpace(enteredNumber))
        {
            CallButton.IsEnabled = false;
            CallButton.Text = "Call";
            return;
        }

        translatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumber);

        if (!string.IsNullOrEmpty(translatedNumber))
        {
            CallButton.IsEnabled = true;
            CallButton.Text = "Call " + translatedNumber;
        }
        else
        {
            CallButton.IsEnabled = false;
            CallButton.Text = "Call";
        }
    }

    async void OnCall(object sender, System.EventArgs e)
    {
        if (await this.DisplayAlert(
            "Dial a Number",
            "Would you like to call " + translatedNumber + "?",
            "Yes",
            "No"))
        {
            try
            {
                if (PhoneDialer.Default.IsSupported)
                    PhoneDialer.Default.Open(translatedNumber);
                else
                    await DisplayAlert("Not Supported", "Phone dialing is not supported on this device.", "OK");
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Not Supported", "Phone dialing is not supported on this device.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Unable to dial", $"Phone dialing failed: {ex.Message}", "OK");
            }
        }
    }
}
