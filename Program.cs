using System;
using System.Collections.Generic;
using LibraryManager;
using LibraryManager.media;
using LibraryManager.service;
using Microsoft.VisualBasic;
using BookSearch = LibraryManager.service.BookSearch;

class Program
{
    static Library library = new Library();
    static FileManager fileManager = new FileManager();
    static BookSearch bookSearch = new BookSearch(library);
    public static string FilePath => "C:\\Users\\Renate Hem\\source\\repos\\LibraryManager\\data\\library_data.json";

    static void Main(string[] args)
    {
      
        InitializeLibrary();
        DisplayWelcomeMessage();

        try
        {
            while (true)
            {
                DisplayMenu();

                string choice = Console.ReadLine();
                ProcessMenuChoice(choice);
            }
        }
        catch (ExitProgramException)
        {
            Console.WriteLine("Exiting program");
        }
    }

    static void InitializeLibrary()
    {
        List<Book> loadedBooks = fileManager.Load(Program.FilePath);
        foreach (var book in loadedBooks)
        {
            library.AddBook(book);
        }
    }
    static void DisplayWelcomeMessage()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Title = "LIBRARY MANAGER";
        Console.WriteLine("\n\n-------------- Welcome to Library Manager --------------\n\n");
        Console.WriteLine($"\nTotalt antall bøker opprettet: {Book.TotalBooks}\n");
        Console.WriteLine("\nPress enter to continue..\n");
        Console.ReadLine();
    }

    static void DisplayMenu()
    {
        Console.ResetColor();
        Console.WriteLine("\n=== Library Menu ===\n");
        Console.WriteLine("1: Add New Book");
        Console.WriteLine("2: Delete Book");
        Console.WriteLine("3: Borrow Book");
        Console.WriteLine("4: Return Book");
        Console.WriteLine("5: Print All Books");
        Console.WriteLine("6: Save library to file");
        Console.WriteLine("7: Load library from file");
        Console.WriteLine("8: Search books");
        Console.WriteLine("9: Exit Program");
        Console.WriteLine();
        Console.Write("Enter your choice: ");
        Console.WriteLine();
    }

    static void ProcessMenuChoice(string choice)
    {
        switch (choice)
        {
            case "1":
                AddItem();
                break;
            case "2":
                DeleteItem();
                break;
            case "3":
                BorrowItem();
                break;
            case "4":
                ReturnItem();
                break;
            case "5":
                library.PrintAllBooks();
                break;
            case "6":
                fileManager.Save(library.Books(), Program.FilePath);
                break;
            case "7":
                fileManager.Load(Program.FilePath);
                break;
            case "8":
                Console.WriteLine("Enter the search term:");
                string searchTerm = Console.ReadLine();
                SearchBooks(searchTerm);
                break;
            case "9":
                throw new ExitProgramException();
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
    }

    public class ExitProgramException : Exception { }
    static void AddItem()
    {
        Console.WriteLine("Is this an Ebook? (Y/N)");
        string isEbook = Console.ReadLine().ToLower();
        bool isEBook = isEbook == "y";

        string title = GetInput("Title: ");
        string author = GetInput("Author: ");
        string isbn = GetISBN();

        Book newBook;

        if (isEBook)
        {
            string filePath = GetInput("File Path: ");
            string fileFormat = GetInput("File Format: ");
            newBook = new EBook(title, author, isbn, filePath, fileFormat);
        }
        else
        {
            newBook = new Book(title, author, isbn);
        }

        library.AddBook(newBook);
    }

    static void DeleteItem()
    {
        string isbn = GetISBN();
        library.DeleteBook(isbn);
    }

    static void BorrowItem()
    {
        string isbn = GetISBN();
        library.BorrowBook(isbn);
    }

    static void ReturnItem()
    {
        string isbn = GetISBN();
        library.ReturnBook(isbn);
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

    static void SearchBooks(string searchTerm)
    {
        List<Book> searchResults = bookSearch.SearchBooks(searchTerm);
        Console.WriteLine("\nSearch Results:\n");

        if (searchResults.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No results found.");
            Console.ResetColor();
        }
        else
        {
            foreach (var book in searchResults)
            {
                book.PrintBookDetails();
            }
        }

        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
}
