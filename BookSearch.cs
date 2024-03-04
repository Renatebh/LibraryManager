﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager.media;
using LibraryManager.service;

namespace LibraryManager
{
    public class BookSearch
    {
        private Library library;

        public BookSearch()
        {
        }

        public BookSearch (Library library)
        {
            this.library = library;
        }
        public List<Book> SearchBooks(string searchTerm)
        {
            List<Book> results = new List<Book>();
            foreach (var book in library.GetBookList())
            {
                if (book.Title.ToLower().Contains(searchTerm.ToLower()))
                {
                    results.Add(book);
                }
            }
            return results;
        }
    }
}
