using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager
{
    public class MediaItem
    {
        public string Title { get; set; }
        public bool IsBorrowed { get; set; }

        public MediaItem(string title, bool isBorrowed) 
        {
            Title = title;
            IsBorrowed = isBorrowed;
        }
    }
}
