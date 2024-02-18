using System;
using System.Collections.Generic;

namespace LibraryManager
{


    public class Library<T> : IStorable<T>
    {
        private List<T> bookList = new List<T>();
        private List<T> borrowedBookList = new List<T>();

        public void AddBook(T book)
        {
            bookList.Add(book);
        }

        public void DeleteBook(T book)
        {
            bookList.Remove(book);
        }

        public void ReturnBook(T book)
        {
            borrowedBookList.Remove(book);
            bookList.Add(book);
        }

        public void BorrowBook(T book)
        {
            borrowedBookList.Add(book);
            bookList.Remove(book);
        }

        public void PrintAvailableBooks()
        {
            Console.WriteLine("=== Available Books ===");
            foreach (var book in bookList)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Title: {((Book)(object)book).Title}");
                Console.WriteLine($"Author: {((Book)(object)book).Author}");
                Console.WriteLine($"ISBN: {((Book)(object)book).ISBN}");
                Console.WriteLine("------------------------");
            }
        }

        public void PrintBorrowedBooks()
        {
            Console.WriteLine("=== Borrowed Books ===");
            foreach (var book in borrowedBookList)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Title: {((Book)(object)book).Title}");
                Console.WriteLine($"Author: {((Book)(object)book).Author}");
                Console.WriteLine($"ISBN: {((Book)(object)book).ISBN}");
                Console.WriteLine("------------------------");
            }
        }

        public void Save(List<T> items, string filePath)
        {
            throw new NotImplementedException();
        }

        public List<T> Load(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
