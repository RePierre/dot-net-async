using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DotNetAsync.Dataflow
{
    public class CancellationExample
    {
        private const int MaxItems = 10;

        public void Run()
        {
            var cts = new CancellationTokenSource();

            Console.WriteLine("Generating first {0} powers of 2.", MaxItems);
            var bufferBlock = new BufferBlock<int>(new DataflowBlockOptions { CancellationToken = cts.Token });
            Enumerable.Range(1, MaxItems)
                .ToList()
                .ForEach(i => bufferBlock.Post(i));
            Console.WriteLine("Scheduling cancellation after 5 seconds.");
            cts.CancelAfter(TimeSpan.FromSeconds(5));

            Console.WriteLine("Creating and linking the remaing blocks to the network.");
            var transformBlock = new TransformBlock<int, double>(i =>
            {
                Thread.Sleep(500);
                return Math.Pow(2, i);
            }, new ExecutionDataflowBlockOptions { BoundedCapacity = 1, CancellationToken = cts.Token });

            var actionBlock = new ActionBlock<double>(async i =>
            {
                await Task.Delay(1000);
                Console.WriteLine(i);
            }, new ExecutionDataflowBlockOptions { BoundedCapacity = 10, CancellationToken = cts.Token });

            bufferBlock.LinkTo(transformBlock, new DataflowLinkOptions { PropagateCompletion = true });
            transformBlock.LinkTo(actionBlock, new DataflowLinkOptions { PropagateCompletion = true });

            var t1 = bufferBlock.Completion.ContinueWith(t => Console.WriteLine("Buffer block status: {0}", t.Status));
            var t2 = actionBlock.Completion.ContinueWith(t => Console.WriteLine("Action block status: {0}", t.Status));
            Console.WriteLine("Waiting for the network to finish.");
            Task.WaitAll(t1, t2);
        }
    }
}