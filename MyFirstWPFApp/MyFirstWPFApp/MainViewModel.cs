using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstWPFApp
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            Title = "Professional C#";
            TheBook = new Book { Title = "Universal Apps", Publisher = "Self" };
        }
        public string Title { get; set; }

        public Book TheBook { get; set; }
    }
}
