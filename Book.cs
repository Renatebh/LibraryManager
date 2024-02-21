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

        public bool IsBorrowed { get; set; }


        // Constructor
        public Book(string title, string author, string isbn)
        {

            Title = title;
            Author = author;
            ISBN = isbn;
            IsBorrowed = false;
        }

        public override bool Equals(object? obj)
        {
            if(obj == null || GetType() != obj.GetType())
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
    }
}
