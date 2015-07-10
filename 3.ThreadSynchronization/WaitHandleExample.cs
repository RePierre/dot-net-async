using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DotNetAsync.ThreadSynchronization
{
    public class WaitHandleExample
    {
        private readonly object _syncRoot = new object();
        private readonly EventWaitHandle _waitHandle = new AutoResetEvent(false);
        private ThreadLocal<Random> _random = new ThreadLocal<Random>(() => new Random());
        private readonly Queue<int> _dataBus = new Queue<int>();
        private bool _finished;

        public void Run()
        {
            new Thread(PrintData).Start();
            for (int i = 0; i < 5; i++)
            {
                GenerateNumbers();
                _waitHandle.Set();
            }
            Console.WriteLine("[Main]: Signaling finish to worker.");
            lock (_syncRoot)
                _finished = true;
            _waitHandle.Set();
        }

        private void GenerateNumbers()
        {
            Console.WriteLine("[Main]: Generating 5 random numbers...");
            var data = Enumerable.Range(1, 5)
                 .Select(_ => _random.Value.Next())
                 .ToList();
            Console.WriteLine("[Main]: Sending numbers [{0}] to data bus.", string.Join(",", data));
            lock (_syncRoot)
            {
                data.ForEach(_dataBus.Enqueue);
            }
        }

        private void PrintData()
        {
            while (true)
            {
                _waitHandle.WaitOne();
                if (_finished)
                    break;

                lock (_syncRoot)
                {
                    Console.WriteLine("[Worker]: Dequeuing 5 numbers.");
                    Console.WriteLine("[Worker]: The numbers are: [{0}].", string.Join(",", Enumerable.Range(1, 5).Select(_ => _dataBus.Dequeue())));
                }
            }
            Console.WriteLine("[Worker]: Finish was signaled. Exiting...");
            _waitHandle.Close();
        }
    }
}