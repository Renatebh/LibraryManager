using System;
using System.Collections.Generic;

namespace LibraryManager
{


    public class Library
    {
        private List<MediaItem> itemList = new List<MediaItem>();

        public void ManageItemAction(MediaItem item, string action, bool isEbook = false)
        {
            switch (action.ToLower())
            {
                case "add":
                    if (!itemList.Contains(item))
                    {
                        itemList.Add(item);
                        Console.WriteLine(DisplayMessage("added"));
                    }
                    else
                    {
                        Console.WriteLine(DisplayMessage("exists"));
                    }
                    break;
                case "delete":
                    if (itemList.Contains(item))
                    {
                        itemList.Remove(item);
                        Console.WriteLine(DisplayMessage("deleted"));
                    }
                    else
                    {
                        Console.WriteLine(DisplayMessage("not found"));
                    }
                    break;
                case "borrow":
                    if (itemList.Contains(item))
                    {
                        item.IsBorrowed = true;
                        Console.WriteLine(DisplayMessage("borrowed"));
                    }
                    else
                    {
                        Console.WriteLine(DisplayMessage("not found"));
                    }
                    break;
                case "return":
                    if (itemList.Contains(item))
                    {
                        item.IsBorrowed = false;
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

        

        public void PrintItems(PrintMode mode = PrintMode.All)
        {
            switch (mode)
            {
                case PrintMode.Available:
                    Console.WriteLine("=== Available Items ===");
                    foreach (var item in itemList)
                    {
                        if (!item.IsBorrowed)
                        {
                            PrintBookDetails(item);
                        }
                    }
                    break;
                case PrintMode.Borrowed:
                    Console.WriteLine("=== Borrowed Items ===");
                    foreach (var item in itemList)
                    {
                        if (item.IsBorrowed)
                        {
                            PrintBookDetails(item);
                        }
                    }
                    break;
                case PrintMode.All:
                    Console.WriteLine("=== All Items ===");
                    foreach (var item in itemList)
                    {
                        PrintBookDetails(item);
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

            if (book is EBook) 
            { 
                EBook eBook = (EBook)book;
                Console.WriteLine($"File Path {eBook.FilePath}");
                Console.WriteLine($"File Format {eBook.FileFormat}");
            }
            Console.WriteLine("\n------------------------\n");
        }


        public enum PrintMode
        {
            Available,
            Borrowed,
            All
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
    }
}
