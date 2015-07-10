using System;
using System.Threading;

namespace DotNetAsync.ThreadSynchronization
{
    public class SecondLockExample
    {
        private static readonly object _syncRoot = new object();
        private bool _alreadyGreeted;

        public void Run()
        {
            new Thread(PrintHello).Start();
            PrintHi();
        }

        private void PrintHi()
        {
            lock (_syncRoot)
            {
                if (!_alreadyGreeted)
                {
                    Console.WriteLine("Hi!");
                    _alreadyGreeted = true;
                }
            }
        }

        private void PrintHello()
        {
            lock (_syncRoot)
            {
                if (!_alreadyGreeted)
                {
                    Console.WriteLine("Hello!");
                    _alreadyGreeted = true;
                }
            }
        }
    }
}