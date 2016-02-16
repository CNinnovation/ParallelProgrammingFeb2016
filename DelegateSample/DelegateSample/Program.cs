using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateSample
{

    public delegate int MathOp(int x, int y);

    class Program
    {
        static void Main(string[] args)
        {
            ACalculator calc = new ACalculator();
            int x = calc.Add(3, 4);

            // MathOp op1 = new MathOp(calc.Add);  // AddressOf calc.Add

            MathOp op1 = calc.Add;

            //int result = op1(3, 2);

            op1 = calc.Subtract;
            // int result2 = op1(3, 2);
            IAsyncResult ar = op1.BeginInvoke(3, 2, CalcCompleted, op1);
//            int result3 = op1.EndInvoke(ar);
            //result = op1(3, 2);

            op1 += calc.Add;

            int result = op1(7, 2);
            Console.WriteLine(result);

            Func<int, int, int> op2 = calc.Add;
            result = op2(11, 3);

            Console.ReadLine();

        }

        static void CalcCompleted(IAsyncResult ar)
        {
            Console.WriteLine("CalcCompleted");
            MathOp op1 = (MathOp)ar.AsyncState;
            int result = op1.EndInvoke(ar);
            Console.WriteLine($"result: {result}");
        }
    }
}
