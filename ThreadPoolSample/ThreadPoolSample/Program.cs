using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
// using static System.Console; C# 6

namespace ThreadPoolSample
{
    class Program
    {
        static void Main(string[] args)
        {
           
            int worker;
            int io;
            ThreadPool.GetAvailableThreads(out worker, out io);

            Console.WriteLine("available Worker: {0}, IO {1}", worker, io);
            ThreadPool.GetMinThreads(out worker, out io);
            Console.WriteLine("min Worker: {0}, IO {1}", worker, io);
            ThreadPool.QueueUserWorkItem(MyAction);
            // ThreadPool.UnsafeQueueNativeOverlapped()
            Console.WriteLine("main thread, main ended {0} is background: {1} pooled: {2}", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsBackground, Thread.CurrentThread.IsThreadPoolThread);
            // C# 6 Console.WriteLine($"main tread, main ended {Thread.CurrentThread.ManagedThreadId} is background: {Thread.CurrentThread.IsBackground}");
            Console.WriteLine("press return to exit");

            Console.ReadLine();
        }

        static void MyAction(object o)
        {
            Console.WriteLine("MyAction thread {0} is background: {1} pooled: {2}", Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsBackground, Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(500);
            Console.WriteLine("pooled thread ended");
        }
    }
}
