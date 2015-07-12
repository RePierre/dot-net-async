using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetAsync.ParallelFramework
{
    public class CancellationExample
    {
        public void Run()
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(5));
            Task.Factory.StartNew(() => PrintCurrentTime(cts.Token), cts.Token).Wait();

            cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromSeconds(6));
            var task = Task.Factory.StartNew(() => GenerateRandomNumbers(cts.Token), cts.Token);
            try
            {
                task.Wait();
            }
            catch (AggregateException ex)
            {
                ex.Handle(e =>
                {
                    if (e is OperationCanceledException)
                    {
                        Console.WriteLine("Random number generation cancelled.");
                        return true;
                    }
                    return false;
                });
            }
        }

        private void PrintCurrentTime(CancellationToken cancellationToken)
        {
            Console.WriteLine("Pooling for cancellation using {0} property.", nameof(CancellationToken.IsCancellationRequested));
            while (!cancellationToken.IsCancellationRequested)
            {
                Console.WriteLine("Current time is: {0}", DateTime.Now.ToLongTimeString());
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            Console.WriteLine("Cancellation was requested. Exiting...");
        }

        private void GenerateRandomNumbers(CancellationToken cancellationToken)
        {
            Console.WriteLine("Checking for cancellation using {0} method.", nameof(CancellationToken.ThrowIfCancellationRequested));
            Random random = new Random();
            int counter = 0;
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                Console.WriteLine("Generating random number {0}: {1}.", counter++, random.Next());
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
            }

            Console.WriteLine("Not going to get here...");
        }
    }
}