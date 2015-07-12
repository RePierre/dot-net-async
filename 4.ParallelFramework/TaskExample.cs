using System;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetAsync.ParallelFramework
{
    public class TaskExample
    {
        private readonly int _count = 1000000;

        public TaskExample(int count = 1000000)
        {
            _count = count;
        }

        public void Run()
        {
            Console.WriteLine("Calculating the sum of the first {0} squared roots...", _count);
            // Task.Factory.StartNew creates a 'hot' task, i.e. a task whose execution is scheduled as soon as possible
            var task = Task.Factory.StartNew(SumOfSquares);
            Console.WriteLine("The result is {0}.", task.Result);

            // To create a 'cold' task uncomment the lines below
            //var coldTask = new Task<double>(SumOfSquares);
            //coldTask.Start();
            //Console.WriteLine("The result is {0}.", coldTask.Result);
        }

        private double SumOfSquares()
        {
            return Enumerable.Range(1, _count)
                .Select(number => Math.Sqrt(number))
                .Sum();
        }
    }
}