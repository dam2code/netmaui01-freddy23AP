using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PartsClient.Data;

public static class PartsManager
{
    // TODO: Add fields for BaseAddress, Url, and authorizationKey
    static readonly string BaseAddress = "URL GOES HERE";
    static readonly string Url = $"{BaseAddress}/api/";
    private static string authorizationKey;

    static HttpClient client;

    private static async Task<HttpClient> GetClient()
    {
        if (client != null)
            return client;

        client = new HttpClient();

        if (string.IsNullOrEmpty(authorizationKey))
        {
            authorizationKey = await client.GetStringAsync($"{Url}login");
            authorizationKey = JsonSerializer.Deserialize<string>(authorizationKey);
        }

        client.DefaultRequestHeaders.Add("Authorization", authorizationKey);
        client.DefaultRequestHeaders.Add("Accept", "application/json");

        return client;
    }

    public static async Task<IEnumerable<Part>> GetAll()
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            return new List<Part>();

        var client = await GetClient();
        string result = await client.GetStringAsync($"{Url}parts");

        return JsonSerializer.Deserialize<List<Part>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        });
    }

    public static async Task<Part> Add(string partName, string supplier, string partType)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            return new Part();

        var part = new Part()
        {
            PartName = partName,
            Suppliers = new List<string> { supplier },
            PartID = string.Empty,  // El servicio web generará este ID
            PartType = partType,
            PartAvailableDate = DateTime.Now.Date
        };

        try
        {
            var client = await GetClient();
            var msg = new HttpRequestMessage(HttpMethod.Post, $"{Url}parts")
            {
                Content = JsonContent.Create(part)
            };

            var response = await client.SendAsync(msg);
            response.EnsureSuccessStatusCode();

            var returnedJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Part>(returnedJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            }) ?? new Part();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error al agregar la pieza: {ex.Message}");
            return new Part();
        }
    }


    public static async Task Update(Part part)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            return;

        if (string.IsNullOrEmpty(part.PartID))
        {
            Console.WriteLine("Error: El ID de la pieza no puede estar vacío.");
            return;
        }

        try
        {
            var client = await GetClient();
            var msg = new HttpRequestMessage(HttpMethod.Put, $"{Url}parts/{part.PartID}")
            {
                Content = JsonContent.Create(part)
            };

            var response = await client.SendAsync(msg);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error al actualizar la pieza: {ex.Message}");
        }
    }


    public static async Task Delete(string partID)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            return;

        if (string.IsNullOrEmpty(partID))
        {
            Console.WriteLine("Error: El ID de la pieza no puede estar vacío.");
            return;
        }

        try
        {
            var client = await GetClient();
            var msg = new HttpRequestMessage(HttpMethod.Delete, $"{Url}parts/{partID}");

            var response = await client.SendAsync(msg);
            response.EnsureSuccessStatusCode();

            Console.WriteLine($"Pieza con ID {partID} eliminada correctamente.");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error al eliminar la pieza: {ex.Message}");
        }
    }

}
