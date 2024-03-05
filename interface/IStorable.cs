using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.@interface
{
    internal interface IStorable<T>
    {
        void Save(IEnumerable<T> items, string filePath);
        List<T> Load(string filePath);
    }
}
