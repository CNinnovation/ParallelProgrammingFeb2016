using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FutureSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<int> t1 = new Task<int>(() =>
            {
                Console.WriteLine($"{nameof(t1)} started");
                Thread.Sleep(1000);
                throw new Exception("bad");
                return 42;
            });
            t1.Start();

            Task<int> t2 = new Task<int>(() =>
            {
                Console.WriteLine($"{nameof(t2)} started");
                Thread.Sleep(1000);

                return 21;
            });

            t2.Start();

            Task t3 = t1.ContinueWith(t =>
            {
                int result = t.Result;
                Console.WriteLine($"result von t1: {result}");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);
            Task t3b = t1.ContinueWith(t =>
            {
                Console.WriteLine("Previous task in faulted state");
            }, TaskContinuationOptions.OnlyOnFaulted);


            //Task t4 = Task<int>.Factory.ContinueWhenAll(new Task[] { t1, t2 }, tasks =>
            //{
            //    foreach (var task in tasks)
            //    {
            //        Task<int> tr = (Task<int>)task;
            //         Console.WriteLine($"result von task: {tr.Result}");
            //    }
            //    return 0;
            //});


            //int x = t1.Result;
            //int y = t2.Result;
            //Console.WriteLine($"results: {x} {y}");
            Console.ReadLine();
        }
    }
}
