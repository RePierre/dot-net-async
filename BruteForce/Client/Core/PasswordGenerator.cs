using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetAsyncExamples.BruteForce.Client.Core
{
    class PasswordGenerator
    {
        #region Fields

        private readonly int _maxPasswordLength;
        private readonly IEnumerable<char> _passwordChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789`~!@#$%^&*()-_=+[]{}\\|;:'\",<.>/?".ToCharArray();

        #endregion

        #region Ctor

        public PasswordGenerator(int maxPasswordLength)
        {
            _maxPasswordLength = maxPasswordLength;
        }

        #endregion

        #region Public Methods

        public IEnumerable<string> Generate()
        {
            Queue<string> queue = new Queue<string>(new string[] { String.Empty });
            while (queue.Count > 0)
            {
                var element = queue.Dequeue();
                if (element.Length >= _maxPasswordLength)
                {
                    yield break;
                }
                foreach (var item in _passwordChars)
                {
                    string password = element + item;
                    queue.Enqueue(password);
                    yield return password;
                }
            }
        }

        #endregion
    }
}
