using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetAsync.ParallelFramework
{
    public class ParallelForEachExample
    {
        private const string Alphabet = @"abcdefghijklmnopqrstuvwxyz";

        private readonly int _maxLineSize = 1000;
        private readonly int _batchSize = 1000000;
        private readonly Random _random = new Random();

        private string[] _data;

        public ParallelForEachExample(int batchSize = 1000000, int maxLineSize = 1000)
        {
            _batchSize = batchSize;
            _maxLineSize = maxLineSize;
        }

        public void Run()
        {
            SeedData();
            SequentialCount();
            ParallelCount();
        }

        private void ParallelCount()
        {
            Console.WriteLine("Counting in parallel...");
            var counts = new ConcurrentDictionary<char, int>(Alphabet.Select(c => new KeyValuePair<char, int>(c, 0)));
            var sw = Stopwatch.StartNew();
            Parallel.ForEach(_data, line =>
            {
                foreach (var group in line.GroupBy(c => c))
                {
                    var count = group.Count();
                    counts.AddOrUpdate(group.Key, count, (c, total) => total + count);
                }
            });
            sw.Stop();
            Console.WriteLine("Total milliseconds elapsed: {0}.", sw.ElapsedMilliseconds);
        }

        private void SeedData()
        {
            Console.Write("Seeding data...");
            _data = Enumerable.Range(1, _batchSize)
                .Select(_ => Enumerable.Range(0, _random.Next(_maxLineSize)).Select(__ => Alphabet[_random.Next(Alphabet.Length)]))
                .Select(chars => new string(chars.ToArray()))
                .ToArray();
            Console.WriteLine("Done.");
        }

        private void SequentialCount()
        {
            Console.WriteLine("Counting sequentially...");
            var counts = Alphabet.ToDictionary(c => c, c => 0);
            var sw = Stopwatch.StartNew();
            foreach (var line in _data)
            {
                foreach (var group in line.GroupBy(c => c))
                {
                    var count = group.Count();
                    counts[group.Key] += count;
                }
            }
            sw.Stop();
            Console.WriteLine("Total milliseconds elapsed: {0}.", sw.ElapsedMilliseconds);
        }
    }
}