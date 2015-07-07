using System;
using System.Threading;

namespace DotNetAsync.ThreadingBasics
{
    public class ParameterizedThreadWithLambdaExpressionExample
    {
        private readonly string _endMessage;
        private readonly string _startMessage;
        private int _times;

        public ParameterizedThreadWithLambdaExpressionExample(int times, string startMessage, string endMessage)
        {
            _times = times;
            _startMessage = startMessage;
            _endMessage = endMessage;
        }

        public void Run()
        {
            var thread = new Thread(() =>
            {
                Console.WriteLine(_startMessage);
                PrintHello(_times);
                Console.WriteLine(_endMessage);
            });
            thread.Start();
            PrintHi(_times);
        }

        private void PrintHello(int times)
        {
            for (int i = 0; i < times; i++)
            {
                Console.WriteLine("[{0}]. Hello!", i);
            }
        }

        private void PrintHi(int times)
        {
            for (int i = 0; i < times; i++)
            {
                Console.WriteLine("[{0}]. Hi!", i);
            }
        }
    }
}
