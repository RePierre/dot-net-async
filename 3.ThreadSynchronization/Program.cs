using System;

namespace DotNetAsync.ThreadSynchronization
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How NOT to access shared resources:");
            var tue = new ThreadUnsafeExample();
            tue.Run();
            Console.WriteLine();

            Console.WriteLine("Accessing shared resurces using <lock>.");
            var fle = new FirstLockExample();
            fle.Run();
            Console.WriteLine();

            Console.WriteLine("Accessing shared resurces using <lock>.");
            var sle = new SecondLockExample();
            sle.Run();
            Console.WriteLine();

            Console.WriteLine("WaitHandle example.");
            var whe = new WaitHandleExample();
            whe.Run();
            Console.WriteLine();

            Console.WriteLine("Two way signaling with producer/consumer.");
            var pce = new ProducerConsumerExample();
            pce.Run();
            Console.WriteLine();
        }
    }
}
