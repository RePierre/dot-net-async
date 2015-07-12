using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetAsync.ParallelFramework
{
    public class TaskContinuationsExample
    {
        private readonly EventWaitHandle _waitHandle = new AutoResetEvent(false);

        public void Run()
        {
            var cts = new CancellationTokenSource();
            var task = Task.Factory.StartNew(() => GenerateRandomNumbers(50, cts.Token), cts.Token);
            task.ContinueWith(_ =>
            {
                Console.WriteLine("The operation was cancelled.");
            }, TaskContinuationOptions.OnlyOnCanceled);

            var validator = task.ContinueWith(ValidateNumbers, TaskContinuationOptions.NotOnCanceled);
            validator.ContinueWith(OnValidationFailure, TaskContinuationOptions.OnlyOnFaulted);
            validator.ContinueWith(OnValidationSuccess, TaskContinuationOptions.OnlyOnRanToCompletion);

            cts.CancelAfter(TimeSpan.FromSeconds(5));
            _waitHandle.WaitOne();
        }

        private static IEnumerable<int> ValidateNumbers(Task<IEnumerable<int>> predecessor)
        {
            var invalid = predecessor.Result.Where(num => num % 21 == 0);
            if (invalid.Any())
            {
                var message = string.Format("Invalid numbers are: [{0}].", string.Join(",", invalid));
                throw new ArgumentException(message);
            }
            return predecessor.Result;
        }

        private void OnValidationSuccess(Task<IEnumerable<int>> predecessor)
        {
            var numbers = string.Join(",", predecessor.Result);
            Console.WriteLine("Generated numbers are: [{0}].", numbers);
            _waitHandle.Set();
        }

        private void OnValidationFailure(Task<IEnumerable<int>> predecessor)
        {
            Console.WriteLine("Some of the generated numbers are invalid.");
            predecessor.Exception.Handle(ex =>
            {
                if (ex is ArgumentException)
                {
                    Console.WriteLine(ex.Message);
                }
                return true;
            });
            _waitHandle.Set();
        }

        private IEnumerable<int> GenerateRandomNumbers(int maxNumbers, CancellationToken cancellationToken)
        {
            Console.WriteLine("Generating random numbers.");
            var random = new Random();
            var results = new List<int>();
            while (!cancellationToken.IsCancellationRequested && results.Count < maxNumbers)
            {
                results.Add(random.Next());
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }
            Console.WriteLine("{0} numbers were generated.", results.Count);
            return results;
        }

    }
}