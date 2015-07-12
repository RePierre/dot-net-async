using System;

namespace DotNetAsync.ParallelLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Parallel Linq example");
            var pe = new ParallelLinqExample();
            pe.Run();
            Console.WriteLine();

            Console.WriteLine("Preserving order with Parallel Linq");
            var ope = new OrderedParallelLinqExample();
            ope.Run();
            Console.WriteLine();

            Console.WriteLine("Cancellation example");
            var ce = new CancellationExample();
            ce.Run();
            Console.WriteLine();

            Console.WriteLine("Parallel aggregations");
            var pae = new ParallelAggregationExample();
            pae.Run();
            Console.WriteLine();
        }
    }
}
