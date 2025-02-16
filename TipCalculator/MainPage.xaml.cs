using System;
using Microsoft.Maui.Controls;

namespace TipCalculator
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            tipPercent.Text = $"{e.NewValue:F0}%";
            CalculateTip();
        }

        private void OnNormalTip(object sender, EventArgs e)
        {
            tipPercentSlider.Value = 15;
        }

        private void OnGenerousTip(object sender, EventArgs e)
        {
            tipPercentSlider.Value = 20;
        }

        private void CalculateTip()
        {
            if (double.TryParse(billInput.Text, out double billAmount))
            {
                double tipPercentage = tipPercentSlider.Value / 100;
                double tip = billAmount * tipPercentage;
                double total = billAmount + tip;

                tipOutput.Text = tip.ToString("0.00");
                totalOutput.Text = total.ToString("0.00");
            }
        }
    }
}
