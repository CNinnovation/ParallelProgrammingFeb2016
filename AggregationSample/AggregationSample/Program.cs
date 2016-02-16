using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AggregationSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ParallelForDemo();
        }

        private static void ParallelForDemo()
        {
            
            List<int> results = new List<int>();
            object syncResults = new object();



            int sum = 0;
            Parallel.For(0, 100,
                () =>
                {
                    ShowInfo("local init");
                    return 0;
                },
                (x, pls, a) =>
                {
                    ShowInfo($"body a: {a}, x: {x}");
                    return a +x;
                },
                a =>
                {
                    ShowInfo($"end a: {a}");
                    lock(syncResults)
                    {
                        results.Add(a);
                    }

                    Interlocked.Add(ref sum, a);
                }
                );

            foreach (var item in results)
            {
                Console.WriteLine(item);
            }
        }

        private static void ShowInfo(string info)
        {
            Console.WriteLine($"{info} {Task.CurrentId} {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
