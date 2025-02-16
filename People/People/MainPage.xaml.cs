using People.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace People;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    // Método OnNewButtonClicked ahora es asincrónico
    public async void OnNewButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        try
        {
            await App.PersonRepo.AddNewPerson(newPerson.Text); // Llamada asincrónica
            statusMessage.Text = App.PersonRepo.StatusMessage;
        }
        catch (Exception ex)
        {
            statusMessage.Text = $"Error: {ex.Message}"; //Se agregó manejo de errores en la UI
        }
    }

    // Método OnGetButtonClicked ahora es asincrónico
    public async void OnGetButtonClicked(object sender, EventArgs args)
    {
        statusMessage.Text = "";

        try
        {
            List<Person> people = await App.PersonRepo.GetAllPeople(); // Llamada asincrónica
            peopleList.ItemsSource = people;
        }
        catch (Exception ex)
        {
            statusMessage.Text = $"Error: {ex.Message}"; //Se agregó manejo de errores en la UI
        }
    }
}
