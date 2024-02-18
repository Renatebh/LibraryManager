using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager
{
    public class Book
    {
        // Properties
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }


        // Constructor
        public Book(string title, string author, string isbn)
        {

            Title = title;
            Author = author;
            ISBN = isbn;
        }
    }
}
