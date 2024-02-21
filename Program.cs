using System;
using LibraryManager;

class Program
{
    static Library library = new Library();

    static void Main(string[] args)
    {
        DisplayWelcomeMessage();


        while (true)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ProcessBookAction(BookAction.Add);
                    break;
                case "2":
                    ProcessBookAction(BookAction.Delete);
                    break;
                case "3":
                    ProcessBookAction(BookAction.Borrow);
                    break;
                case "4":
                    ProcessBookAction(BookAction.Return);
                    break;
                case "5":
                    library.PrintAvailableBooks();
                    break;
                case "6":
                    library.PrintBorrowedBooks();
                    break;
                case "7":
                    Console.WriteLine("Search books");
                    return;
                case "8":
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
        Console.WriteLine("2: Delete Book");
        Console.WriteLine("3: Borrow Book");
        Console.WriteLine("4: Return Book");
        Console.WriteLine("5: Print available books");
        Console.WriteLine("6: Print borrowed books");
        Console.WriteLine("7: Search books");
        Console.WriteLine("8: Exit");
        Console.Write("\nEnter your choice: \n");
    }

    static void ProcessBookAction(BookAction action)
    {
        Console.ResetColor();
        string actionString = action.ToString().ToLower();
        Console.WriteLine($"\nEnter the details of the book you want to {actionString}: \n");

        string title = GetInput("Title: ");
        string author = GetInput("Author: ");
        string isbn = GetISBN();

        Book newBook = new Book(title, author, isbn);

        Console.ForegroundColor = ConsoleColor.Green;
        switch (action)
        {
            case BookAction.Add:
                library.ManageBookActions(newBook, "add");
                break;
            case BookAction.Delete:
                library.ManageBookActions(newBook, "delete");
                break;
            case BookAction.Borrow:
                library.ManageBookActions(newBook, "borrow");
                break;
            case BookAction.Return:
                library.ManageBookActions(newBook, "return");
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

public enum BookAction
{
    Add,
    Delete,
    Borrow,
    Return
}
