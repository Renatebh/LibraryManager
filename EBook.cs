using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager
{
    public class EBook: Book
    {
        public string FilePath { get; set; }
        public string FileFormat { get; set; }

        public EBook(string title, string author, string isbn, string filePath, string fileFormat)
            : base(title, author, isbn)
        {
            FilePath = filePath;
            FileFormat = fileFormat;
        }

   
    }

}
