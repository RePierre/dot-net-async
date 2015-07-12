using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DotNetAsync.Dataflow
{
    public class ErrorHandlingExample
    {
        public void Run()
        {
            Console.WriteLine("Generating first 10 powers of 2.");
            var bufferBlock = new BufferBlock<int>();
            Enumerable.Range(1, 10)
                .ToList()
                .ForEach(i => bufferBlock.Post(i));


            var transformBlock = new TransformBlock<int, double>(i =>
            {
                Console.WriteLine("Raising 2 to the power of {0}.", i);
                if (i == 5)
                {
                    Console.WriteLine("32 is so mainstream... Throwing exception...");
                    throw null;
                }

                return Math.Pow(2, i);
            }, new ExecutionDataflowBlockOptions { BoundedCapacity = 1 });

            var actionBlock = new ActionBlock<double>(async i =>
            {
                await Task.Delay(500);
                Console.WriteLine(i);
            }, new ExecutionDataflowBlockOptions { BoundedCapacity = 10 });

            var completion = actionBlock.Completion.ContinueWith(t =>
            {
                Console.WriteLine("Processing failed.");
                Console.WriteLine(t.Exception.Message);
            }, TaskContinuationOptions.OnlyOnFaulted);

            var options = new DataflowLinkOptions { PropagateCompletion = true };
            bufferBlock.LinkTo(transformBlock, options);
            transformBlock.LinkTo(actionBlock, options);

            completion.Wait();
        }
    }
}