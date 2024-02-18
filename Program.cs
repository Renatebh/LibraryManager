using System;
using LibraryManager;

class Program
{
    static Library<Book> library = new Library<Book>();

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
        Console.WriteLine("\n\n--------------Welcome to Library manager--------------\n\n");
        Console.WriteLine("\n\nPress enter to continue\n");
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
        Console.Write("Enter your choice: ");
    }

    static void ProcessBookAction(BookAction action)
    {
        Console.ResetColor();

        string actionString = action.ToString().ToLower();
        Console.WriteLine($"Enter the details of the book you want to {actionString}");
       
        string title;
        do
        {

            Console.WriteLine("Title: ");
            title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Title cannot be empty. Please try again");
            }
        } while (string.IsNullOrWhiteSpace(title));

       
        string author;
        do
        {
            Console.WriteLine("Author: "); 
            author = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(author))
            {
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Author cannot be empty. Please try again");
            }
        } while (string.IsNullOrWhiteSpace(author));

      
        string isbn;
        bool isValidIsbn;
        do
        {
            Console.WriteLine("ISBN: must contain 10 or 13 characters");
            isbn = Console.ReadLine();

            isValidIsbn = ISBNValidator.IsValidISBN(isbn);
            if (!isValidIsbn)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("Invalid ISBN. Must contain 10 or 13 characters. Please try again.");
            }
        } while (!isValidIsbn);

        Book newBook = new Book(title, author, isbn);

        Console.ForegroundColor = ConsoleColor.Green;
        switch (action)
        {
            case BookAction.Add:
                library.AddBook(newBook);
                    Console.WriteLine("Book added successfully.");
                break;
            case BookAction.Delete:
                library.DeleteBook(newBook);
                Console.WriteLine("Book deleted successfully.");
                break;
            case BookAction.Borrow:
                library.BorrowBook(newBook);
                Console.WriteLine("Book borrowed successfully.");
                break;
            case BookAction.Return:
                library.ReturnBook(newBook);
                Console.WriteLine("Book returned successfully.");
                break;
        }
    }
}

public enum BookAction
{
    Add,
    Delete,
    Borrow,
    Return
}
