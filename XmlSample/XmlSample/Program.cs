using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XmlSample
{
    class MyData
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {

            XElement racers = XElement.Load("http://www.cninnovation.com/downloads/Racers.xml");
            var q = from r in racers.Elements("Racer")
                    where r.Element("Country").Value == "Brazil" && int.Parse(r.Element("Wins").Value) > 18
                    select new 
                    {
                        Name = r.Element("Firstname").Value + " " + r.Element("Lastname").Value,
                        Country = r.Element("Country").Value
                    };

            foreach (var r in q)
            {
                Console.WriteLine($"{r.Name}, {r.Country}");
            }

            int x = 3;
            Foo(b: x, a: x);

            XNamespace ns = "www.xy.z";
            var books = new XElement(ns + "Books",
                new XElement("Book", new XAttribute("Title", "Professional C# 6"),
                                     new XAttribute("Publisher", "Wrox Press"),
                                     new XElement("Content", "Pages")));

            Console.WriteLine(books);

            dynamic exp1 = new ExpandoObject();
            exp1.FirstName = "Tom";
            exp1.LastName = "Turbo";
            exp1.Method = new Func<string>(() => "abc");

            Console.WriteLine(exp1.FirstName);

            string abc = exp1.Method();

            //dynamic p = new Program();
            //dynamic x2 = p.CoolBar();
            //Console.WriteLine(x2);
           
        }

        public int Bar()
        {
            return 42;
        }

        static void Foo(int a, int b)
        {

        }
    }
}
