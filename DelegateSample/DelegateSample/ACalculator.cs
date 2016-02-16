using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DelegateSample
{
    public class ACalculator
    {
        public int Add(int x, int y) => x + y;

        public int Subtract(int x, int y)
        {
            Thread.Sleep(4000);
            return x - y;
        }
    }
}
