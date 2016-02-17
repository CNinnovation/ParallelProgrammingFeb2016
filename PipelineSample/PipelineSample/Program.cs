using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipelineSample
{
    class Program
    {
        private static BlockingCollection<SomeData> coll1 = new BlockingCollection<SomeData>();
        static void Main(string[] args)
        {
            FillTask();
            ReadTask();
            Console.ReadLine();
        }

        private static void ReadTask()
        {
            Task.Run(async () =>
            {
                await Task.Delay(1000);

                foreach (var item in coll1.GetConsumingEnumerable())
                {
                    Console.WriteLine($"read {item.Text}");
                }
                Console.WriteLine("read completed");
            });
        }

        private static void FillTask()
        {
            Task.Run(async () =>
            {
                for (int i = 0; i < 200; i++)
                {
                    coll1.Add(new SomeData { Number = i, Text = $"text {i}" });
                    Console.WriteLine($"write {i}");
                    await Task.Delay(20);

                }
                coll1.CompleteAdding();
            });
        }
    }
}
