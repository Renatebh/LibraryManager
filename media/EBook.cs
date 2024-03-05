using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.media
{
    public class EBook : Book
    {
        public string FilePath { get; set; }
        public string FileFormat { get; set; }

        public EBook(string title, string author, string isbn, string filePath, string fileFormat)
     : base(title, author, isbn, false)
        {
            FilePath = filePath;
            FileFormat = fileFormat;
        }


        public override void PrintBookDetails()
        {
            base.PrintBookDetails(); // Kaller metoden i foreldreklassen
            Console.WriteLine($"File Path: {FilePath}");
            Console.WriteLine($"File Format: {FileFormat}");
            Console.WriteLine("\n------------------------\n");
        }
    }

}
