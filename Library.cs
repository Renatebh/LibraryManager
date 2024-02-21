using System;
using System.Collections.Generic;

namespace LibraryManager
{


    public class Library
    {
        private List<Book> bookList = new List<Book>();

        public void ManageBookAction(Book book, string action)
        {
            switch (action.ToLower())
            {
                case "add":
                    if (!bookList.Contains(book))
                    {
                        bookList.Add(book);
                        Console.WriteLine(DisplayMessage("added"));
                    }
                    else
                    {
                        Console.WriteLine(DisplayMessage("exists"));
                    }
                    break;
                case "delete":
                    if (bookList.Contains(book))
                    {
                        bookList.Remove(book);
                        Console.WriteLine(DisplayMessage("deleted"));
                    }
                    else
                    {
                        Console.WriteLine(DisplayMessage("not found"));
                    }
                    break;
                case "borrow":
                    if (bookList.Contains(book))
                    {
                        bookList[bookList.IndexOf(book)].IsBorrowed = true; 
                        Console.WriteLine(DisplayMessage("borrowed"));
                    }
                    else
                    {
                        Console.WriteLine(DisplayMessage("not found"));
                    }
                    break;
                case "return":
                    if (bookList.Contains(book))
                    {
                        bookList[bookList.IndexOf(book)].IsBorrowed = false;
                        Console.WriteLine(DisplayMessage("returned"));
                    }
                    else
                    {
                        Console.WriteLine(DisplayMessage("not found"));
                    }
                    break;

                default:
                    Console.WriteLine("Invalid action");
                    break;
            }
        }

        public static string DisplayMessage(string action)
        {
            switch (action.ToLower())
            {
                case "added":
                    return "Book added successfully";
                case "deleted":
                    return "Book deleted successfully";
                case "not found":
                    return "Book not found in the library";
                case "exists":
                    return "Book already exists in the library";
                case "borrowed":
                    return "Book borrowed successfully";
                case "returned":
                    return "Book returned successfully";
                default:
                    return "Invalid action";
            }
        }

        public void PrintBooks(PrintMode mode = PrintMode.All)
        {
            switch (mode)
            {
                case PrintMode.Available:
                    Console.WriteLine("=== Available Books ===");
                    foreach (var book in bookList)
                    {
                        if (!book.IsBorrowed)
                        {
                            PrintBookDetails(book);
                        }
                    }
                    break;
                case PrintMode.Borrowed:
                    Console.WriteLine("=== Borrowed Books ===");
                    foreach (var book in bookList)
                    {
                        if (book.IsBorrowed)
                        {
                            PrintBookDetails(book);
                        }
                    }
                    break;
                case PrintMode.All:
                    Console.WriteLine("=== All Books ===");
                    foreach (var book in bookList)
                    {
                        PrintBookDetails(book);
                    }
                    break;
                default:
                    Console.WriteLine("Invalid printing mode");
                    break;
            }
        }

        private void PrintBookDetails(Book book)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Title: {book.Title}");
            Console.WriteLine($"Author: {book.Author}");
            Console.WriteLine($"ISBN: {book.ISBN}");
            Console.WriteLine("------------------------");
        }


        public enum PrintMode
        {
            Available,
            Borrowed,
            All
        }
    }
}
