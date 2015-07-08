using System;

namespace DotNetAsync.ThreadingBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. Creating & starting a simple thread");
            var ste = new SimpleThreadExample();
            ste.Run();
            Console.WriteLine();

            Console.WriteLine("Creating & starting a thread with parameters");
            var pte = new ParameterizedThreadExample(parameter: 10);
            pte.Run();
            Console.WriteLine();

            Console.WriteLine("Passing parameters using lambda expressions");
            var lete = new ParameterizedThreadWithLambdaExpressionExample(times: 10, startMessage: @"Before print", endMessage: @"After print");
            lete.Run();
            Console.WriteLine();

            Console.WriteLine("Exception handling");
            var ehe = new ExceptionHandlingExample();
            ehe.Run();
            Console.WriteLine();
        }
    }
}
