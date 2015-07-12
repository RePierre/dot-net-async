using System;

namespace DotNetAsync.Dataflow
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Simple network example");
            var sne = new SimpleNetworkExample();
            sne.Run();
            Console.WriteLine();

            Console.WriteLine("Signaling completion");
            var ce = new CompletionExample();
            ce.Run();
            Console.WriteLine();

            Console.WriteLine("Cancellation example");
            var cancellation = new CancellationExample();
            cancellation.Run();
            Console.WriteLine();

            Console.WriteLine("Exception propagation through the pipeline.");
            var eh = new ErrorHandlingExample();
            eh.Run();
            Console.WriteLine();

            Console.WriteLine("Dealing with concurrency in TPL Dataflow");
            var csepe = new ConcurrentExclusiveSchedulerPairExample();
            csepe.Run();
            Console.WriteLine();

            Console.WriteLine("Creating custom blocks");
            var ee = new EncapsulateExample();
            ee.Run();
            Console.WriteLine();
        }
    }
}
