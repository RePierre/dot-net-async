using System;
using System.Threading;

namespace DotNetAsync.ThreadPooling
{
    public class ParameterizedThreadPoolExample
    {
        private int _times;

        public ParameterizedThreadPoolExample(int times)
        {
            _times = times;
        }

        public void Run()
        {
            ThreadPool.QueueUserWorkItem(PrintHello, _times);
            Console.WriteLine("[{0}]: Press <Enter> to continue...", GetType().Name);
            Console.ReadLine();
        }

        private void PrintHello(object state)
        {
            for (int i = 0; i < (int)state; i++)
            {
                Console.WriteLine("Hello!");
            }
        }
    }
}
