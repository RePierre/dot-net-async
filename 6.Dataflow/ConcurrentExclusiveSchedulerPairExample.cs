using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DotNetAsync.Dataflow
{
    public class ConcurrentExclusiveSchedulerPairExample
    {
        public void Run()
        {
            var propagateCompletion = new DataflowLinkOptions { PropagateCompletion = true };

            var csep = new ConcurrentExclusiveSchedulerPair();
            var concurrentOptions = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = 2,
                TaskScheduler = csep.ConcurrentScheduler
            };

            var exclusiveOptions = new ExecutionDataflowBlockOptions
            {
                TaskScheduler = csep.ExclusiveScheduler
            };

            var concurrent = new ActionBlock<int>(async i =>
            {
                Console.WriteLine("Concurent print value: {0}", i);
                await Task.Delay(500);
            }, concurrentOptions);

            var exclusive = new ActionBlock<int>(i =>
            {
                Console.WriteLine("Exclusive print value: {0}", i);
                Thread.Sleep(750);
            }, exclusiveOptions);

            var broadcaster = new BroadcastBlock<int>(i => i);
            broadcaster.LinkTo(concurrent, propagateCompletion);
            broadcaster.LinkTo(exclusive, propagateCompletion, i => i % 2 == 0);

            Enumerable
              .Range(1, 10)
              .ToList()
              .ForEach(i => broadcaster.Post(i));
            broadcaster.Complete();
            Task.WaitAll(concurrent.Completion, exclusive.Completion);
        }
    }
}