using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DotNetAsync.ParallelLinq
{
    public class CancellationExample
    {
        private string GeneSequence
        {
            get
            {
                var alphabet = "agct";
                var random = new Random();
                var genome = Enumerable.Range(1, 1000000)
                    .Select(_ => alphabet[random.Next(alphabet.Length)])
                    .ToArray();
                return new string(genome);
            }
        }

        public void Run()
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(100);
            Console.WriteLine("Calculating tetranucleotides distribution.");
            try
            {
                var results = GeneSequence.AsParallel()
                                    .WithCancellation(cts.Token)
                                    .Aggregate(
                                        () => new Dictionary<char, int>(),
                                        UpdatePartitionAccumulator,
                                        CombineAccumulators,
                                        totals => totals
                                    );
                Console.WriteLine("Not going to get here.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("The operation was cancelled.");
            }
        }

        private Dictionary<char, int> UpdatePartitionAccumulator(Dictionary<char, int> partitionAccumulator, char letter)
        {
            if (!partitionAccumulator.ContainsKey(letter))
            {
                partitionAccumulator.Add(letter, 0);
            }
            partitionAccumulator[letter]++;
            return partitionAccumulator;
        }

        private Dictionary<char, int> CombineAccumulators(Dictionary<char, int> globalTotals, Dictionary<char, int> partitionTotals)
        {
            return partitionTotals.Select(kvp => new
            {
                kvp.Key,
                Value = globalTotals.ContainsKey(kvp.Key) ? globalTotals[kvp.Key] + kvp.Value : kvp.Value
            })
            .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}