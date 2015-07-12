using System;

namespace DotNetAsync.ParallelFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Using Parallel.Foreach:");
            var pfe = new ParallelForEachExample(batchSize: 10000, maxLineSize: 100);
            pfe.Run();
            Console.WriteLine();

            Console.WriteLine("Creating and starting a task.");
            var te = new TaskExample();
            te.Run();
            Console.WriteLine();

            Console.WriteLine("Establishing relationships between the tasks.");
            var pce = new ParentChildExample();
            pce.Run();
            Console.WriteLine();

            Console.WriteLine("Exception handling with tasks.");
            var ehe = new ExceptionHandlingExample();
            ehe.Run();
            Console.WriteLine();

            Console.WriteLine("Cancelling tasks.");
            var ce = new CancellationExample();
            ce.Run();
            Console.WriteLine();

            Console.WriteLine("Task continuations.");
            var tce = new TaskContinuationsExample();
            tce.Run();
            Console.WriteLine();
        }
    }
}
