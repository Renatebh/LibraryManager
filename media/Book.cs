using System;

namespace LibraryManager.media
{
    public class Book
    {
        public static int TotalBooks {get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public bool IsEbook { get; set; }
        public bool IsBorrowed { get; set; }


        public Book(string title, string author, string isbn, bool isEbook, bool isBorrowed)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            IsEbook = isEbook;
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
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("This book is available.");
            }
            Console.ResetColor();
            if (!IsEbook)
            {
                Console.WriteLine("\n-------------\n");
            }
        }

    }
}
