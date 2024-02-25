using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager
{
    public class Movie : MediaItem
    {
        public string Director {  get; set; }


        public Movie(string title, bool isBorrowed, string director) : base (title, isBorrowed)
        {
            Director = director;
        }

    }

}
