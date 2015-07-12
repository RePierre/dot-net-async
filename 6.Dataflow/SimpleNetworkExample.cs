using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DotNetAsync.Dataflow
{
    public class SimpleNetworkExample
    {
        private const int MaxItems = 10;

        private readonly CountdownEvent _waitHandle = new CountdownEvent(MaxItems);

        public void Run()
        {
            Console.WriteLine("Generating first {0} powers of 2.", MaxItems);
            var bufferBlock = new BufferBlock<int>();
            var transformBlock = new TransformBlock<int, double>(i =>
            {
                Thread.Sleep(500);
                return Math.Pow(2, i);
            }, new ExecutionDataflowBlockOptions { BoundedCapacity = 10 });

            var actionBlock = new ActionBlock<double>(async i =>
            {
                await Task.Delay(1000);
                Console.WriteLine(i);
                _waitHandle.Signal();
            }, new ExecutionDataflowBlockOptions { BoundedCapacity = 10 });

            bufferBlock.LinkTo(transformBlock);
            transformBlock.LinkTo(actionBlock);

            Enumerable.Range(1, MaxItems)
                .ToList()
                .ForEach(i => bufferBlock.Post(i));

            _waitHandle.Wait();
        }
    }
}