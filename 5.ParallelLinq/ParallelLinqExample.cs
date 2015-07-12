using System;
using System.Linq;

namespace DotNetAsync.ParallelLinq
{
    public class ParallelLinqExample
    {
        private const string Alphabet = @"abcdefghijklmnopqrstuvwxyz";
        private readonly Random _random = new Random();

        public void Run()
        {
            Console.WriteLine("Generating random text...");
            var lines = Enumerable.Range(1, 1000000)
                .AsParallel()
                .Select(number =>
                {
                    var lineLength = _random.Next(10, 100);
                    var line = Enumerable.Range(1, lineLength)
                        .Select(_ => Alphabet[_random.Next(Alphabet.Length)])
                        .ToArray();
                    return new
                    {
                        Index = number,
                        Text = new string(line)
                    };
                });
            Console.WriteLine("Printing first 10 lines.");
            lines.Take(10)
                .ToList()
                .ForEach(line => Console.WriteLine("{0:##} {1}", line.Index, line.Text));
        }
    }
}