using SQLite;
using People.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace People;

public class PersonRepository
{
    private SQLiteAsyncConnection conn; // Se mantiene SQLiteAsyncConnection para consultas asincrónicas
    private readonly string _dbPath;

    public string StatusMessage { get; set; }

    // Método Init ahora es asincrónico
    private async Task Init()
    {
        if (conn != null)
            return;

        conn = new SQLiteAsyncConnection(_dbPath);
        await conn.CreateTableAsync<Person>(); // Creación de la tabla de forma asincrónica
    }

    public PersonRepository(string dbPath)
    {
        _dbPath = dbPath;
        // Se eliminó Init().Wait(); para evitar bloqueos en el constructor
        // En su lugar, Init() se llamará en cada método antes de operar con la base de datos.
    }

    // Método AddNewPerson ahora es asincrónico
    public async Task AddNewPerson(string name)
    {
        int result = 0;
        try
        {
            await Init(); // Asegura que la conexión esté inicializada antes de operar

            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");

            result = await conn.InsertAsync(new Person { Name = name }); // Inserción asincrónica

            StatusMessage = $"{result} record(s) added (Name: {name})";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Failed to add {name}. Error: {ex.Message}";
        }
    }

    // Método GetAllPeople ahora es asincrónico
    public async Task<List<Person>> GetAllPeople()
    {
        try
        {
            await Init(); // Asegura que la conexión esté inicializada antes de operar

            return await conn.Table<Person>().ToListAsync(); // Recuperación asincrónica
        }
        catch (Exception ex)
        {
            StatusMessage = $"Failed to retrieve data. {ex.Message}";
            throw; //Se agregó `throw;` para permitir que el error se propague
        }

        return new List<Person>();

        //Se eliminó Init().Wait(); en el constructor para evitar bloqueos.
        //Se agregó throw; en GetAllPeople() para manejar errores correctamente.
        //Se mejoró el manejo de errores en los botones(OnNewButtonClicked y OnGetButtonClicked).
    }
}
