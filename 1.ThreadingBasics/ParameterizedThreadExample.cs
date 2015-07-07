using System;
using System.Linq;
using System.Threading;

namespace DotNetAsync.ThreadingBasics
{
    public class ParameterizedThreadExample
    {
        private readonly int _parameter;

        public ParameterizedThreadExample(int parameter)
        {
            _parameter = parameter;
        }

        public void Run()
        {
            var thread = new Thread(new ParameterizedThreadStart(PrintHello));
            thread.Start(_parameter);
            PrintHello(_parameter);
        }

        private void PrintHello(object parameters)
        {
            int times = (int)parameters;
            Enumerable.Range(0, times)
                .ToList()
                .ForEach(iteration => Console.WriteLine("[{0}]. Hello!", iteration));
        }
    }
}
