using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Other;
using System.Collections.Concurrent;

namespace Other
{
    static class BlubBlub
    {
        public static void Foo(this string s)
        {
            Console.WriteLine($"Foo {s}");
        }

        public static void Foo(this DateTime dt)
        {
            Console.WriteLine(dt.Day);
        }
    }
}

namespace ParallelDemo
{

   
    static class Blabla
    {
        public static void Foo(this string s)
        {
            Console.WriteLine($"Foo {s}");
        }

        public static void Foo(this DateTime dt)
        {
            Console.WriteLine(dt.Day);
        }
    }



    class Program
    {


        static void Main(string[] args)
        {
           // ThreadPool.SetMinThreads(50, 10);
            Console.WriteLine($"main thread background: {Thread.CurrentThread.IsBackground} thread: {Thread.CurrentThread.ManagedThreadId} task: {Task.CurrentId}");
            // ParallelLoop();

            // ParallelForEachDemo();
            // ParallelLinqDemo();

            //string s = "sample";
            //s.Foo();

            //DateTime dt = DateTime.Today;
            //dt.Foo();

            //Blabla.Foo(s);

            // BreakingOutOfLoops();
            // UseCancellationToken();

            // BreakingOutOfLoopsWithExceptions();

            ParallelInvoke();


        }

        private static void ParallelInvoke()
        {
            Parallel.Invoke(Foo, Bar, Foo, Foo);
            Console.WriteLine($"{nameof(ParallelInvoke)} completed");
        }

        public static void Foo()
        {
            ShowTaskInfo(nameof(Foo));
        }

        public static void Bar()
        {
            ShowTaskInfo(nameof(Bar));
        }

        private static void UseCancellationToken()
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(3000);
            cts.Token.Register(() => Console.WriteLine("cancel now!!!!!!"));

            try
            {
                Parallel.For(0, 1000, new ParallelOptions { CancellationToken = cts.Token },
                    x =>
                    {
                        Console.WriteLine($"starting {x}");
                        Thread.Sleep(400);

                        Console.WriteLine($"ending {x}");
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().Name);
            }
        }

        private static void BreakingOutOfLoopsWithExceptions()
        {
            try
            {
                Parallel.For(0, 1000, (x, pls) =>
                {
                    Console.WriteLine(x);
                    //if (pls.ShouldExitCurrentIteration)
                    //{
                    //    Console.WriteLine("...should exit");
                    //    return;
                    //}

                    if (x > 300) throw new Exception("some error");

                });
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.GetType().Name);
                foreach (var ex1 in ex.InnerExceptions)
                {
                    Console.WriteLine($"{ex1.GetType().Name}, {ex1.Message}");
                }
            }
        }

        private static void BreakingOutOfLoops()
        {
            try
            {
                Parallel.For(0, 1000, (x, pls) =>
                {
                    Console.WriteLine(x);
                    if (pls.ShouldExitCurrentIteration)
                    {
                        Console.WriteLine("...should exit");
                        return;
                    }

                    if (x > 300) pls.Break();

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ParallelLinqDemo()
        {
            var q = from sd in GetSampleData().AsParallel()
                    where sd.Number > 40
                    select sd;

            var list = GetSampleData().AsParallel().WithDegreeOfParallelism(2).Where(sd => sd.Number > 40).OrderBy(sd => sd.Number).Select(sd => sd).ToList();

        }

        private static void ParallelForEachDemo()
        {
            var data = GetSampleData();

            Parallel.ForEach(data, sd =>
            {
                Console.WriteLine(sd);
            });
        
        }

        private static void ParallelLoop()
        {
            Parallel.For(0, 1000, new ParallelOptions() { MaxDegreeOfParallelism = 2 }, x =>
            {
                ShowTaskInfo($"ParallelLoop {x}");
                Thread.Sleep(10);
                ShowTaskInfo($"ParallelLoop end {x}");
            });
        }

        private static void ShowTaskInfo(string info)
        {
            Console.WriteLine($"{info} background: {Thread.CurrentThread.IsBackground} pool: {Thread.CurrentThread.IsThreadPoolThread} thread: {Thread.CurrentThread.ManagedThreadId} task: {Task.CurrentId}");
        }

        private static IEnumerable<SampleData> GetSampleData()
        {
            var r = new Random();
            return Enumerable.Range(0, 1000).Select(x => new SampleData { Text = $"Text{x}", Number = r.Next(100) }).ToList();
        }
    }
}
