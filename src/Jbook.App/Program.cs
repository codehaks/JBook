// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using Npgsql;


Console.WriteLine("Hello, World!");

string jsonFilePath = @"C:\Projects\Dev\JBookDemo\books.json"; // Replace with your JSON file path

List<string> books=ReadJsonFile(jsonFilePath);

//string jsonData = File.ReadAllText(jsonFilePath);

//List<string> books = JsonConvert.DeserializeObject<List<string>>(jsonData);

string connectionString = "Host=localhost;Username=postgres;Password=6859;Database=JBook_db";

using var connection = new NpgsqlConnection(connectionString);

try
{
    connection.Open();
    Console.WriteLine("Connected to PostgreSQL!");

    //string insertQuery = @"INSERT INTO public.""Book""(""Data"") VALUES (@BookData)";
    string sqlCommand = @"INSERT INTO public.""Book"" (""Data"") VALUES (@BookData);";


    using var command = new NpgsqlCommand(sqlCommand, connection);

    // Insert multiple records using a for-loop
    foreach (var book in books.Where(b=>b.Length>0))
    {
        // Add parameters to the query
        command.Parameters.Clear(); // Clear parameters from previous iteration
        command.Parameters.AddWithValue("@BookData", book);

        int rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            Console.WriteLine($"Data inserted successfully!");
        }
        else
        {
            Console.WriteLine($"Failed to insert data!");
        }
    }

    connection.Close();
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}

static List<string> ReadJsonFile(string filePath)
{
    List<string> listOfStrings = new List<string>();

    try
    {
        string json = File.ReadAllText(filePath);
        List<Dictionary<string, object>> jsonData = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(json);

        foreach (var item in jsonData)
        {
            string serializedObject = JsonConvert.SerializeObject(item, Formatting.Indented);
            listOfStrings.Add(serializedObject);
            listOfStrings.Add(""); // Add an empty line between objects
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error reading the JSON file: {ex.Message}");
    }

    return listOfStrings;
}