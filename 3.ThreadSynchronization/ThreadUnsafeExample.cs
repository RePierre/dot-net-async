using System;
using System.Threading;

namespace DotNetAsync.ThreadSynchronization
{
    public class ThreadUnsafeExample
    {
        private bool _alreadyGreeted = false;

        public void Run()
        {
            new Thread(PrintHello).Start();
            PrintHi();
        }

        private void PrintHi()
        {
            if (!_alreadyGreeted)
            {
                Console.WriteLine("Hi!");
                _alreadyGreeted = true;
            }
        }

        private void PrintHello()
        {
            if (!_alreadyGreeted)
            {
                Console.WriteLine("Hello!");
                _alreadyGreeted = true;
            }
        }
    }
}
