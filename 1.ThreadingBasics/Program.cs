using System;

namespace DotNetAsync.ThreadingBasics
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Creating & starting a simple thread
            Console.WriteLine("Simple thread example:");
            var ste = new SimpleThreadExample();
            ste.Run();
            Console.WriteLine();

            // 2. Creating & starting a thread with parameters
            Console.WriteLine("Parameterized thread example:");
            var pte = new ParameterizedThreadExample(parameter: 10);
            pte.Run();
            Console.WriteLine();

            // 3. Passing parameters using lambda expresion
            Console.WriteLine("Passing parameters using lambda expressions:");
            var lete = new ParameterizedThreadWithLambdaExpressionExample(times: 10, startMessage: @"Before print", endMessage: @"After print");
            lete.Run();
            Console.WriteLine();

            // 4. Exception handling
            Console.WriteLine("Exception handling example:");
            var ehe = new ExceptionHandlingExample();
            ehe.Run();
            Console.WriteLine();
        }
    }
}
