using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DotNetAsync.Dataflow
{
    public class CompletionExample
    {
        private const int MaxItems = 10;

        public void Run()
        {
            Console.WriteLine("Generating first {0} powers of 2.", MaxItems);
            var bufferBlock = new BufferBlock<int>();
            Enumerable.Range(1, MaxItems)
                .ToList()
                .ForEach(i => bufferBlock.Post(i));

            Console.WriteLine("Signaling completion to the source block.");
            bufferBlock.Complete();
            Console.WriteLine("Done.");

            Console.WriteLine("Creating and linking the remaing blocks to the network.");
            var transformBlock = new TransformBlock<int, double>(i =>
            {
                Thread.Sleep(200);
                return Math.Pow(2, i);
            }, new ExecutionDataflowBlockOptions { BoundedCapacity = 1 });

            var actionBlock = new ActionBlock<double>(async i =>
            {
                await Task.Delay(500);
                Console.WriteLine(i);
            }, new ExecutionDataflowBlockOptions { BoundedCapacity = 10 });

            bufferBlock.LinkTo(transformBlock, new DataflowLinkOptions { PropagateCompletion = true });
            transformBlock.LinkTo(actionBlock, new DataflowLinkOptions { PropagateCompletion = true });

            Console.WriteLine("Waiting for the completion to be propagated through the network...");
            actionBlock.Completion.ContinueWith(t =>
            {
                Console.WriteLine("Finished processing.");
                Console.WriteLine(string.Format("Completion status: {0}.", t.Status));
            }).Wait();
        }
    }
}