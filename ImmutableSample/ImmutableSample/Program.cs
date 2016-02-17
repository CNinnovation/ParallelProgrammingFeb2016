using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmutableSample
{

    class Program
    {
        static void Main(string[] args)
        {
            List<string> list1 = new List<string>() { "one", "two", "three" };
            ImmutableList<string> list2 = list1.ToImmutableList();

            Console.WriteLine("1st iteration");
            foreach (var item in list2)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            ImmutableList<string> list3 = list2.Add("four");

            Console.WriteLine("2nd iteration");
            foreach (var item in list3)
            {
                Console.WriteLine(item);
            }




        }
    }
}
