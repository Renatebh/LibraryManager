using System;
using LibraryManager.media;
using LibraryManager.service;
using Microsoft.VisualBasic;

class Program
{
    static Library library = new Library();
    static FileManager fileManager = new FileManager();

    public static string FilePath { get; } = "C:\\Users\\Renate Hem\\source\\repos\\LibraryManager\\data\\library_data.json";

    static void Main(string[] args)
    {

        List<Book> loadedBooks = fileManager.Load(Program.FilePath);
        foreach (var book in loadedBooks)
        {
            library.ManageBookAction(book, BookAction.AddBook); // Legg til boken i biblioteket
        }

        DisplayWelcomeMessage();
      

        while (true)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ProcessAction(BookAction.AddBook);
                    break;
                case "2":
                    ProcessAction(BookAction.AddEBook);
                    break;
                case "3":
                    ProcessAction(BookAction.Delete);
                    break;
                case "4":
                    ProcessAction(BookAction.DeleteEBook);
                    break;
                case "5":
                    ProcessAction(BookAction.Borrow);
                    break;
                case "6":
                    ProcessAction(BookAction.Return);
                    break;
                case "7":
                    library.SortBooks(Library.SortByTitle);
                    library.PrintAllBooks();
                    break;
                case "8":
                    library.SortBooks(Library.SortByTitle);
                    library.PrintAllEBooks();
                    break;
                case "9":
                    fileManager.Save(library.GetBookList(), Program.FilePath);
                    break;
                case "10":
                    fileManager.Load(Program.FilePath);
                    break;
                case "11":
                    Console.WriteLine("Exiting program");
                    return;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }

    }

    static void DisplayWelcomeMessage()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Title = "LIBRARY MANAGER";
        Console.WriteLine("\n\n-------------- Welcome to Library manager --------------\n\n");
        Console.WriteLine("\n\nPress enter to continue\n\n");
        Console.ReadLine();
    }


    static void DisplayMenu()
    {
        Console.ResetColor();
        Console.WriteLine("\n=== Library Manager Menu ===\n");
        Console.WriteLine("1: Add New Book");
        Console.WriteLine("2: Add New E-Book");
        Console.WriteLine("3: Delete Book");
        Console.WriteLine("4: Delete E-Book");
        Console.WriteLine("5: Borrow Book");
        Console.WriteLine("6: Return Book");
        Console.WriteLine("7: Print All Books");
        Console.WriteLine("8: Print All E-Books");
        Console.WriteLine("9: Save library to file");
        Console.WriteLine("10: Load library from file");
        Console.WriteLine("11: Exit Program");
        Console.WriteLine();
        Console.Write("Enter your choice: ");
        Console.WriteLine();
    }
    
    static void ProcessAction(BookAction action)
    {
        Console.ResetColor();
        string actionString = action.ToString().ToLower();
        Console.WriteLine("\n\n----------------------------\n\n");
        Console.WriteLine($"\nEnter the details of the book you want to {actionString}: \n");
        Console.ForegroundColor = ConsoleColor.White;
        string title = GetInput("Title: ");
        string author = GetInput("Author: ");

        string isbn = GetISBN();
        bool isEbook = action == BookAction.AddEBook;
        // Inkluder feilsøkningsutskrift for å bekrefte at bokobjektene blir opprettet korrekt
        // Console.WriteLine($"Creating new book: Title={title}, Author={author}, ISBN={isbn}, IsEbook={isEbook}");

        // Inkluder feilsøkningsutskrift for å bekrefte hvilken handling som blir utført
        // Console.WriteLine($"Performing action: {action}");
  

        switch (action)
        {
            case BookAction.AddBook:
            case BookAction.Delete:
            case BookAction.Borrow:
            case BookAction.Return:
                bool isBorrowed = action == BookAction.Borrow;
                Book newBook = new Book(title, author, isbn, isEbook, isBorrowed); 
                library.ManageBookAction(newBook, action);
                break;
            case BookAction.AddEBook:
            case BookAction.DeleteEBook:
               
                string filePath = GetInput("File Path: ");
                string fileFormat = GetInput("File Format: ");
                EBook newEBook = new EBook(title, author, isbn, filePath, fileFormat); 
                library.ManageBookAction(newEBook, action);
                break;
            default:
                Console.WriteLine("Invalid action");
                break;
        }
    }
    static string GetInput(string prompt)
    {
        string input;
        do
        {
            Console.WriteLine(prompt);
            input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{prompt.TrimEnd(':')} cannot be empty. Please try again");
            }
        } while (string.IsNullOrWhiteSpace(input));
        return input;
    }

    static string GetISBN()
    {
        string isbn;
        bool isValidIsbn;
        do
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("ISBN (10 or 13 characters): ");
            isbn = Console.ReadLine();

            isValidIsbn = ISBNValidator.IsValidISBN(isbn);
            if (!isValidIsbn)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid ISBN. Please try again");
            }
        } while (!isValidIsbn);
        return isbn;
    }
}

public enum BookAction
{
    AddBook,
    PrintBook,
    Delete,
    Borrow,
    Return,
    AddEBook,
    DeleteEBook
}
