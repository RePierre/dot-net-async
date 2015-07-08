using System;

namespace DotNetAsync.ThreadPooling
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. ThreadPool example");
            var tpe = new ThreadPoolExample();
            tpe.Run();
            Console.WriteLine();

            Console.WriteLine("2. Using ThreadPool with parameters");
            var ptpe = new ParameterizedThreadPoolExample(5);
            ptpe.Run();
            Console.WriteLine();

            Console.WriteLine("3. Using Asynchronous delegates");
            var ade = new AsyncDelegatesExample();
            ade.Run();
            Console.WriteLine();
        }
    }
}
