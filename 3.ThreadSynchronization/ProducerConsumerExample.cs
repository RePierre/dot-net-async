using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DotNetAsync.ThreadSynchronization
{
    public class ProducerConsumerExample
    {
        private const int BatchSize = 3;
        private readonly object _syncRoot = new object();
        private readonly EventWaitHandle _dataAwailable = new AutoResetEvent(false);
        private readonly EventWaitHandle _dataRequired = new AutoResetEvent(false);
        private readonly ThreadLocal<Random> _random = new ThreadLocal<Random>(() => new Random());
        private readonly Queue<int> _dataBus = new Queue<int>();
        private bool _finished;

        public void Run()
        {
            var producer = new Thread(Producer);
            producer.Start();
            var consumer = new Thread(Consumer);
            consumer.Start();

            // Wait for the threads to finish.
            producer.Join();
            consumer.Join();

            // Cleanup resources.
            _dataAwailable.Close();
            _dataRequired.Close();
        }

        private void Producer()
        {
            int counter = 0;
            while (counter < 5)
            {
                Console.WriteLine("[{0}]: Waiting for data request...", nameof(Producer));
                _dataRequired.WaitOne();
                Console.WriteLine("[{0}]: Generating {1} random numbers.", nameof(Producer), BatchSize);
                lock (_syncRoot)
                {
                    Enumerable.Range(1, BatchSize)
                        .Select(_ => _random.Value.Next())
                        .ToList()
                        .ForEach(_dataBus.Enqueue);
                }
                counter++;
                _dataAwailable.Set();
            }

            Console.WriteLine("[{0}]: Signaling finish.", nameof(Producer));
            lock (_syncRoot)
                _finished = true;
        }

        private void Consumer()
        {
            Console.WriteLine("[{0}]: Consumer is alive! Requesting data...", nameof(Consumer));
            _dataRequired.Set();
            while (!_finished)
            {
                Console.WriteLine("[{0}]: Waiting for data...", nameof(Consumer));
                _dataAwailable.WaitOne();
                Console.WriteLine("[{0}]: Processing data.", nameof(Consumer));
                lock (_syncRoot)
                {
                    var data = string.Join(",", Enumerable.Range(1, BatchSize).Select(_ => _dataBus.Dequeue()));
                    Console.WriteLine("[{0}]: Received [{1}].", nameof(Consumer), data);
                }
                _dataRequired.Set();
            }
        }
    }
}