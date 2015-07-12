using System;
using System.Linq;

namespace DotNetAsync.ParallelLinq
{
    public class ParallelAggregationExample
    {
        public void Run()
        {
            // The unseeded aggregation methods are intended to be used with functions that are
            // associative and commutative.
            // Our function f(total, n) => total + n * n is neither.
            Console.WriteLine("Calculating the sum of squares for first 10 numbers sequentially.");
            // This will provide the correct result because first item in the sequence is 0
            // which will act as a seed value.
            var sum = Enumerable.Range(0, 10).Aggregate((total, n) => total + n * n);
            Console.WriteLine("Result: {0}", sum);

            Console.WriteLine("Calculating the sum of squares for first 10 numbers in parallel.");
            var parallelSum = 0;
            // Sometimes the partitioner will partition the elements in a proper order which results in parallelSum == sum
            // We will do multiple runs until the partitioner performs differently
            do
            {
                parallelSum = Enumerable.Range(0, 10).AsParallel().Aggregate((total, n) => total + n * n);
            } while (parallelSum == sum);
            Console.WriteLine("Result: {0}", parallelSum);

            Console.WriteLine("Calculating the sum of squares for first 10 numbers in parallel using local accumulators for each partition.");
            var correctParallelSum = Enumerable.Range(0, 10).AsParallel()
                .Aggregate(
                    seedFactory: () => 0,
                    updateAccumulatorFunc: (partitionTotal, number) => partitionTotal + number * number,
                    combineAccumulatorsFunc: (globalTotal, partitionTotal) => globalTotal + partitionTotal,
                    resultSelector: total => total);
            Console.WriteLine("Result: {0}", correctParallelSum);
        }
    }
}