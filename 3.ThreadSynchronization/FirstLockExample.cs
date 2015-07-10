using System;
using System.Threading;

namespace DotNetAsync.ThreadSynchronization
{
    public class FirstLockExample
    {
        private static readonly object _syncRoot = new object();

        private bool _alreadyGreeted;

        public bool AlreadyGreeted
        {
            get
            {
                // Lock here also when using reference types.
                return _alreadyGreeted;
            }
            set
            {
                lock (_syncRoot)
                {
                    _alreadyGreeted = value;
                }
            }
        }

        public void Run()
        {
            new Thread(PrintHello).Start();
            PrintHi();
        }

        private void PrintHi()
        {
            if (!AlreadyGreeted)
            {
                Console.WriteLine("Hi!");
                AlreadyGreeted = true;
            }
        }

        private void PrintHello()
        {
            if (!AlreadyGreeted)
            {
                Console.WriteLine("Hello!");
                AlreadyGreeted = true;
            }
        }
    }
}