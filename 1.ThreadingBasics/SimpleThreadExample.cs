using System;
using System.Linq;
using System.Threading;

namespace DotNetAsync.ThreadingBasics
{
    public class SimpleThreadExample
    {
        public void Run()
        {
            var thread = new Thread(PrintHello);
            thread.Start();
            PrintHello();
        }

        private void PrintHello()
        {
            Enumerable.Range(0, 5)
                .ToList()
                .ForEach(iteration => Console.WriteLine("[{0}]. Hello!", iteration));
        }
    }
}
