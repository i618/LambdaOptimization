using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;
using LambdaFuncOptimization;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<int, int, int> someDelegate = (int x, int y) => SomeFunc(x) > SomeFunc(y) ? SomeFunc(x) : SomeFunc(y);

            someDelegate(1000, 1500);
            Console.WriteLine("--------");

            someDelegate = Optimization.Run<Func<int, int, int>>(
                (int x, int y) => SomeFunc(x) > SomeFunc(y) ? SomeFunc(x) : SomeFunc(y)
            );

            someDelegate(1000, 1500);


            Action someDelegate1 = Optimization.Run<Action>(
                () => SomeFunc1()
            );

            Console.ReadLine();
        }

        [LambdaFuncOptimization]
        private static int SomeFunc(int param)
        {
            Console.WriteLine(String.Format("Ждем {0} мс", param));
            Thread.Sleep(param);
            return param;
        }

        [LambdaFuncOptimization]
        private static void SomeFunc1()
        {
            // ;
        }

    }
}
