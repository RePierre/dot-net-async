using System;
using System.Threading;

namespace DotNetAsync.ThreadPooling
{
    public class ThreadPoolExample
    {
        public void Run()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(PrintHello));
            PrintHi();
            Console.WriteLine("[{0}]: Press <Enter> to continue...", typeof(ThreadPoolExample).Name);
            Console.ReadLine();
        }

        private void PrintHi()
        {
            Console.WriteLine("Hi!");
        }

        private void PrintHello(object state)
        {
            Console.WriteLine("Hello!");
        }
    }
}
