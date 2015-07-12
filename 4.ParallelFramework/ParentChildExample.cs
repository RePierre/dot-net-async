using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetAsync.ParallelFramework
{
    public class ParentChildExample
    {
        private readonly int _maxChildren;
        private readonly int _maxNumbers;
        private readonly Random _random = new Random();

        public ParentChildExample(int maxNumbers = 1000000, int maxChildren = 4)
        {
            _maxNumbers = maxNumbers;
            _maxChildren = maxChildren;
        }

        public IEnumerable<IGrouping<int, int>> Partitions
        {
            get
            {
                return Enumerable.Range(1, _maxNumbers)
                                .Select(_ => _random.Next())
                                .GroupBy(x => x % _maxChildren);
            }
        }

        public void Run()
        {
            Console.WriteLine("Calculating the minimum of {0} randomly generated numbers using {1} detached child tasks.", _maxNumbers, _maxChildren);
            Task<int> globalMinCalculator = CalculateGlobalMinimum();
            globalMinCalculator.Wait();
            Console.WriteLine("The minimum is: {0}.", globalMinCalculator.Result);

            Console.WriteLine("Printing the local minimum of {0} randomly generated numbers split into {1} partitions.", _maxNumbers, _maxChildren);
            Task localMinimumPrinter = PrintLocalMinima();
            localMinimumPrinter.Wait();
            Console.WriteLine("Finished.");
        }

        private Task<int> CalculateGlobalMinimum()
        {
            return Task.Factory.StartNew(() =>
            {
                var tasks = Partitions
                      .Select(g => Task.Factory.StartNew(() =>
                      {
                          var data = g.ToArray();
                          return data.Min();
                      }))
                      .ToArray();
                return tasks.Min(t => t.Result);
            });
        }

        private Task PrintLocalMinima()
        {
            return Task.Factory.StartNew(() =>
            {
                foreach (var g in Partitions)
                {
                    Task.Factory.StartNew(() =>
                    {
                        var context = new { WorkerNumber = g.Key, Data = g.ToArray() };
                        Console.WriteLine("Local minimum {0}: {1}.", context.WorkerNumber, context.Data.Min());
                    }, TaskCreationOptions.AttachedToParent);
                }
            });
        }
    }
}