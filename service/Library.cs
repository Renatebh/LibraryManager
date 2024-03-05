using System;
using System.Collections.Generic;
using LibraryManager.media;

namespace LibraryManager.service
{
    public class Library
    {
        private List<Book> bookList = new List<Book>();
        public Library()
        {

            AddDefaultBooks();
        }
        private void AddDefaultBooks()
        {

            AddBook(new Book("The Great Gatsby", "F. Scott Fitzgerald", "978-0743273565"));
            AddBook(new Book("To Kill a Mockingbird", "Harper Lee", "978-0061120084"));
            AddBook(new Book("1984", "George Orwell", "978-0451524935"));
            AddBook(new Book("Pride and Prejudice", "Jane Austen", "978-0141439518"));
        }



        public void ManageBookAction(Book item, BookAction action)
        {
            switch (action)
            {
                case BookAction.AddBook:
                    AddBook(item);
                    break;
                case BookAction.AddEBook:
                    AddEBook((EBook)item);
                    break;
                case BookAction.Delete:
                    DeleteBook(item);
                    break;
                case BookAction.DeleteEBook:
                    DeleteEBook((EBook)item);
                    break;
                case BookAction.Borrow:
                    BorrowBook(item);
                    break;
                case BookAction.Return:
                    ReturnBook(item);
                    break;
                default:
                    Console.WriteLine("Invalid action");
                    break;
            }
        }

        public delegate int BookComparer(Book b1, Book b2);


        public static BookComparer SortByTitle = (b1, b2) => string.Compare(b1.Title, b2.Title);


        public static BookComparer SortByAuthor = (b1, b2) => string.Compare(b1.Author, b2.Author);

        public void SortBooks(BookComparer comparer)
        {
            bookList.Sort((b1, b2) => comparer(b1, b2));
        }


        public IEnumerable<Book> Books()
        {
            return bookList.AsEnumerable();
        }
        private void AddBook(Book book)
        {
            // Console.WriteLine($"Adding/deleting book to/from library: {bookList.Contains(book)}");
            // Console.WriteLine($"Checking if book exists in library: {bookList.Contains(book)}");
            if (!bookList.Contains(book))
            {
                bookList.Add(book);
                Console.WriteLine(DisplayMessage("added"));
            }
            else
            {
                Console.WriteLine(DisplayMessage("exists"));
            }
        }

        private void AddEBook(EBook eBook)
        {
            if (!bookList.Contains(eBook))
            {
                bookList.Add(eBook);
                Console.WriteLine(DisplayMessage("added"));
            }
            else
            {
                Console.WriteLine(DisplayMessage("exists"));
            }
        }

        private void DeleteBook(Book book)
        {
            if (bookList.Contains(book))
            {
                bookList.Remove(book);
                Console.WriteLine(DisplayMessage("deleted"));
            }
            else
            {
                Console.WriteLine(DisplayMessage("not found"));
            }
        }

        private void DeleteEBook(EBook eBook)
        {
            if (bookList.Contains(eBook))
            {
                bookList.Remove(eBook);
                Console.WriteLine(DisplayMessage("deleted"));
            }
            else
            {
                Console.WriteLine(DisplayMessage("not found"));
            }
        }

        private void BorrowBook(Book book)
        {
            Book foundBook = bookList.Find(b => b.Equals(book));
            if (foundBook != null)
            {
                if (!foundBook.IsBorrowed)
                {
                    foundBook.IsBorrowed = true;
                    Console.WriteLine(DisplayMessage("borrowed"));
                }
                else
                {
                    Console.WriteLine("This book is already borrowed.");
                }
            }
            else
            {
                Console.WriteLine(DisplayMessage("not found"));
            }
        }

        private void ReturnBook(Book book)
        {
            Book foundBook = bookList.Find(b => b.Equals(book));
            if (foundBook != null)
            {
                if (foundBook.IsBorrowed) // Sjekk om boken er lånt ut før du setter IsBorrowed til false
                {
                    foundBook.IsBorrowed = false;
                    Console.WriteLine(DisplayMessage("returned"));
                }
                else
                {
                    Console.WriteLine("This book is not currently borrowed.");
                }
            }
            else
            {
                Console.WriteLine(DisplayMessage("not found"));
            }
        }

        public void PrintAllBooks()
        {
            foreach (var book in bookList)
            {
                book.PrintBookDetails();
            }
        }

        public void PrintAllEBooks()
        {
            foreach (var book in bookList.OfType<EBook>())
            {
                    book.PrintBookDetails();
            }
        }
        private string DisplayMessage(string action)
        {
            switch (action.ToLowerInvariant())
            {
                case "added":
                    return "\nBook added successfully\n";
                case "deleted":
                    return "\nBook deleted successfully\n";
                case "not found":
                    return "\nBook not found in the libraryn\n";
                case "exists":
                    return "\nBook already exists in the library\n";
                case "borrowed":
                    return "\nBook borrowed successfully\n";
                case "returned":
                    return "\nBook returned successfully\n";
                default:
                    return "\nInvalid actionn\n";
            }
        }
    }
}
