using LibraryManager.@interface;
using LibraryManager.media;
using System.Reflection;
using System.Text.Json;

public class FileManager : IStorable<Book>
{
    public void Save(IEnumerable<Book> books, string fileName)
    {
        string directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        string filePath = Path.Combine(directory, fileName);

        var opt = new JsonSerializerOptions()
        {
            WriteIndented = true
        };
        string json = JsonSerializer.Serialize(books);
        File.WriteAllText(filePath, json);
        Console.WriteLine("Library saved to file successfully.");
    }

    public List<Book> Load(string fileName)
    {
        List<Book> books = new List<Book>();
        string directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        string filePath = Path.Combine(directory, fileName);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            books = JsonSerializer.Deserialize<List<Book>>(json);
            Console.WriteLine("Library loaded from file successfully.");
        }
        else
        {
            Console.WriteLine("File does not exist.");
        }
        return books;
    }
}
