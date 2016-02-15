using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SyncSample
{
    class Program
    {
        static void Main(string[] args)
        {

            Thread t1 = new Thread(Thread1);
            Thread t2 = new Thread(Thread1);
            t1.Start();
            t2.Start();
            Console.WriteLine("press return to exit");
            Console.ReadLine();
        }

        static int s_shared = 5;
        static object s_lockshared = new object();

        static void Thread1()
        {
            lock(s_lockshared)
            {
                for (int i = 0; i < 100; i++)
                {

                    s_shared++;
                }
            }

            Monitor.Enter(s_lockshared);
            try
            {
                for (int i = 0; i < 100; i++)
                {

                    s_shared++;
                }
            }
            finally
            {
                Monitor.Exit(s_lockshared);
            }

            bool isMyLock = Monitor.TryEnter(s_lockshared);
            if (isMyLock)
            {
                try
                {
                    // use the resource
                }
                finally
                {
                    Monitor.Exit(s_lockshared);
                }

            }
            else
            {
                // do something else
            }
        }
    }
}
