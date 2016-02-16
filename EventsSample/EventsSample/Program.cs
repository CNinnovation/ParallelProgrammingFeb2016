using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsSample
{
    class Program
    {
        static void Main(string[] args)
        {
            CarFactory factory = new CarFactory();
            Consumer fernando = new Consumer("Fernando");
            factory.CarCreated += fernando.ACarIsHere;
            factory.CreateACar("Mercedes");
            Consumer seb = new Consumer("Seb");
            factory.CarCreated += seb.ACarIsHere;
            try
            {
                factory.CreateACar("McLaren");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            factory.CarCreated -= fernando.ACarIsHere;
            factory.CreateACar("Ferrari");
        }
    }
}
