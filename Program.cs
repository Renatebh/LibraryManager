using System;
using LibraryManager;
using Microsoft.VisualBasic;

class Program
{
    static Library library = new Library();

    static void Main(string[] args)
    {
        DisplayWelcomeMessage();
        GetIsEbook();

        while (true)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ProcessItemAction(ItemAction.AddBook);
                    break;
                case "2":
                    ProcessItemAction(ItemAction.AddMovie);
                    break;
                case "3":
                    ProcessItemAction(ItemAction.Delete);
                    break;
                case "4":
                    ProcessItemAction(ItemAction.Borrow);
                    break;
                case "5":
                    ProcessItemAction(ItemAction.Return);
                    break;
                case "6":
                    //library.SearchItems();
                    break;
                case "7":
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
        Console.WriteLine("=== Library Manager Menu ===");
        Console.WriteLine("1: Add New Book");
        Console.WriteLine("2: Add New Movie");
        Console.WriteLine("3: Delete Item");
        Console.WriteLine("4: Borrow Item");
        Console.WriteLine("5: Return Item");
        Console.WriteLine("6: Print all items");
        Console.WriteLine("7: Print borrowed Items");
        Console.WriteLine("8: Search Items");
        Console.WriteLine("9: Exit");
        Console.Write("\nEnter your choice: \n");
    }

    static void ProcessItemAction(ItemAction action)
    {
        Console.ResetColor();
        string actionString = action.ToString().ToLower();
        Console.WriteLine($"\nEnter the details of the item you want to {actionString}: \n");

        string title = GetInput("Title: ");
        string author = GetInput("Author: ");
        string authorOrDirector = GetInput(action == ItemAction.AddBook ? "Author: " : "Director: ");
        string isbn = GetISBN();
        bool isEbook = action == ItemAction.AddBook ? GetIsEbook() : false;

        if (action == ItemAction.AddBook)
        {
            Book newBook = new Book(title, author, isbn, isEbook);
            library.ManageItemAction(newBook, action);
        }
        else if (action == ItemAction.AddMovie)
        {
            Movie newMovie = new Movie(title, false, director);
            library.ManageItemAction(newMovie, action);
        }
    }

    static bool GetIsEbook()
    {
        Console.WriteLine("Is this an ebook? (Y/N)");
        string input = Console.ReadLine().Trim().ToUpper();

        while (input != "Y" && input != "N")
        {
            Console.WriteLine("Invalid input. Use Y or N");
            input = Console.ReadLine().Trim().ToUpper();
        }
        return input == "Y";
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
            Console.WriteLine("\nISBN: must contain 20 or 13 characters\n");
            isbn = Console.ReadLine();

            isValidIsbn = ISBNValidator.IsValidISBN(isbn);
            if (!isValidIsbn)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid ISBN. Must contain 10 or 13 characters. Please try again");
            }
        } while (!isValidIsbn);
        return isbn;
    }
}

public enum ItemAction
{
    AddBook,
    AddMovie,
    Delete,
    Borrow,
    Return
}
