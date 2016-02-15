using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSample
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Thread t1 = new Thread(MyThread);
            t1.SetApartmentState(ApartmentState.STA);
            t1.IsBackground = false;
            t1.Start();
            Console.WriteLine("end of main");
        }

        static void MyThread()
        {
            Console.WriteLine("running in a thread");
            Thread.Sleep(500);
            Console.WriteLine("finished MyThread");
        }
    }
}
