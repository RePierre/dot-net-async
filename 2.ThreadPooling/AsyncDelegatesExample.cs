using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetAsync.ThreadPooling
{
    public class AsyncDelegatesExample
    {
        public void Run()
        {
            Func<IEnumerable<int>, int, IEnumerable<double>> method = RaiseToPower;
            Console.WriteLine("Scheduling async raising of numbers 1-10 to power 4.");
            IAsyncResult asyncResult = method.BeginInvoke(Enumerable.Range(1, 10), 4, null, null);
            Console.WriteLine("Raising numbers 1-10 to power 3..");
            var result = RaiseToPower(Enumerable.Range(1, 10), 3);
            Console.WriteLine("Result: {0}.", String.Join(",", result));
            result = method.EndInvoke(asyncResult);
            Console.WriteLine("Numbers 1-10 raised to power 4: {0}.", String.Join(",", result));
        }

        private IEnumerable<double> RaiseToPower(IEnumerable<int> numbers, int power)
        {
            return numbers.Select(n => Math.Pow(n, power))
                .ToArray();
        }
    }
}
