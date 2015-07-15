using DotNetAsyncExamples.BruteForce.Client.Core.Resources;
using DotNetAsyncExamples.BruteForce.Domain.Entities;
using DotNetAsyncExamples.BruteForce.Server;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetAsyncExamples.BruteForce.Client.Core
{
    class HackerEngine
    {
        private int _maxPasswordLength;
        private BlockingCollection<string> _pipe = new BlockingCollection<string>();
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly BlockingCollection<string> _messagePipe = new BlockingCollection<string>();

        public HackerEngine()
            : this(new CancellationTokenSource(), 4)
        {
        }

        public HackerEngine(CancellationTokenSource cancellationTokenSource, int maxPasswordLength)
        {
            _maxPasswordLength = maxPasswordLength;
            _cancellationTokenSource = cancellationTokenSource;
        }

        public BlockingCollection<string> MessagePipe
        {
            get
            {
                return _messagePipe;
            }
        }

        public void Run()
        {
            CancellationToken token = _cancellationTokenSource.Token;
            Task.Factory.StartNew(GeneratePasswords, token);
            var tasks = new List<Task>();
            for (int i = 0; i < Environment.ProcessorCount; i++)
            {
                var task = Task.Factory.StartNew(state =>
                {
                    BruteForce(state, token);
                }, i, token);
                tasks.Add(task);
            }

            Task.WhenAll(tasks)
                .ContinueWith(x =>
                {
                    MessagePipe.Add(EngineStrings.MessageAllWorkersFinished);
                    MessagePipe.CompleteAdding();
                });

        }


        private void BruteForce(object state, CancellationToken token)
        {
            string name = state.ToString();
            try
            {
                foreach (var password in _pipe.GetConsumingEnumerable(token))
                {
                    var bytes = Encoding.Unicode.GetBytes(password);
                    var hash = MD5.Create().ComputeHash(bytes);
                    string message = String.Format(EngineStrings.MessageTryingPassword, name, password);
                    MessagePipe.Add(message);
                    if (TryLogin("admin", Encoding.Unicode.GetString(hash)))
                    {
                        message = String.Format(EngineStrings.MessageLoginSucceeded, name, password);
                        MessagePipe.Add(message);
                        _cancellationTokenSource.Cancel();
                        return;
                    }
                    message = String.Format(EngineStrings.MessageLoginFailed, name, password);
                    MessagePipe.Add(message);
                }
            }
            catch (OperationCanceledException)
            {
                MessagePipe.Add(String.Format(EngineStrings.MessageCancellationRequested, name));
                return;
            }
            MessagePipe.Add(String.Format(EngineStrings.MessageWorkerFinished, name));
        }

        private bool TryLogin(string username, string password)
        {
            try
            {
                RemoteServer.Login(new User
                {
                    Username = username,
                    Password = password
                });
                return true;
            }
            catch (AuthenticationException)
            {
                return false;
            }
        }

        private void GeneratePasswords()
        {
            var generator = new Core.PasswordGenerator(_maxPasswordLength);
            try
            {
                foreach (var password in generator.Generate())
                {
                    _pipe.Add(password);
                }
            }
            finally
            {
                _pipe.CompleteAdding();
            }
        }

    }
}
