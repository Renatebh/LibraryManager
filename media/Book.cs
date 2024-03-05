using System;
using System.Text.Json.Serialization;

namespace LibraryManager.media
{ 
[JsonDerivedType(typeof(Book), typeDiscriminator: "book")]
[JsonDerivedType(typeof(EBook), typeDiscriminator: "ebook")]
public class Book
    {
        public static int TotalBooks {get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public bool IsBorrowed { get; set; }


        public Book(string title, string author, string isbn, bool isBorrowed = false)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            IsBorrowed = isBorrowed;

            TotalBooks++;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Book otherBook = (Book)obj;

            return ISBN.Equals(otherBook.ISBN, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return ISBN.GetHashCode();
        }

        public virtual void PrintBookDetails()
        {
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"ISBN: {ISBN}");
            Console.WriteLine($"IsBorrowed: {IsBorrowed}");

            if (IsBorrowed)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("This book is currently borrowed.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("This book is available.");
                Console.ResetColor();
            }
            if (this is EBook)
            {
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("\n------------------------\n");
            } 
          
        }

    }
}
