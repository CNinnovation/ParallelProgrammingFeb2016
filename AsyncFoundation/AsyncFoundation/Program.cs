using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncFoundation
{
    class Program
    {
        static void Main(string[] args)
        {
            // Run();
            RunErrors();
            Console.WriteLine("Main completed");
            Console.ReadLine();
        }

        static async void Run()
        {
            TaskInfo("Run started");
            //string s1 = await GreetingAsync(3000, "Stephanie");
            //string s2 = await GreetingAsync(2000, "Matthias");
            Task<string> t1 = GreetingAsync(3000, "Stephanie");
            Task<string> t2 = GreetingAsync(2000, "Matthias");
            await Task.WhenAll(t1, t2);
            Console.WriteLine(t1.Result);
            Console.WriteLine(t2.Result);
            TaskInfo("Run completed");
        }

        static async void RunErrors()
        {
            Task returnTask = null;
            try
            {
                Task t1 = ErrorAfterAsync(2000, "one");
                Task t2 = ErrorAfterAsync(1000, "two");
                await (returnTask = Task.WhenAll(t2, t1));
             
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                foreach (var innerex in returnTask.Exception.InnerExceptions)
                {
                    Console.WriteLine(innerex.Message);
                }
            }
            Console.WriteLine("RunErrors completed");

        }

        static void TaskInfo(string info)
        {
            Console.WriteLine($"{info} {Task.CurrentId} {Thread.CurrentThread.ManagedThreadId}");
        }

        static Task<string> GreetingAsync(int ms, string name)
        {
            return Task.Run(() =>
            {
                string result = Greeting(ms, name);
                return result;
            });
        }

        static string Greeting(int ms, string name)
        {
            Thread.Sleep(ms);
            // await Task.Delay(ms);
            return "Hello, " + name;
        }

        static Task ErrorAfterAsync(int ms, string message)
        {
            return Task.Run(() =>
            {
                ErrorAfter(ms, message);
            });
        }

        static void ErrorAfter(int ms, string message)
        {
            Thread.Sleep(ms);
            throw new Exception(message);
        }
    }
}
