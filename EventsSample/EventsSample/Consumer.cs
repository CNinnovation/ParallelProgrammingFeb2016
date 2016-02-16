using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsSample
{
    class Consumer
    {
        private string _name;
        public Consumer(string name)
        {
            _name = name;
        }

        public void ACarIsHere(string car)
        {
            if (_name == "Fernando" && car == "McLaren") throw new Exception("unexpected");
            Console.WriteLine($"{_name} {car}");
        }
    }
}
