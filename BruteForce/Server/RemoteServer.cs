using DotNetAsyncExamples.BruteForce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetAsyncExamples.BruteForce.Server.LocalResources;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Collections;
using System.Threading;
using System.Diagnostics;

namespace DotNetAsyncExamples.BruteForce.Server
{
    class RemoteServer
    {
        #region Fields

        private static Lazy<RemoteServer> _instance = new Lazy<RemoteServer>(true);
        private ConcurrentDictionary<string, string> _users = new ConcurrentDictionary<string, string>();
        private static readonly Random _random = new Random(Guid.NewGuid().GetHashCode());
        private static readonly object _syncRoot = new object();
        private readonly char[] _passwordChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789`~!@#$%^&*()-_=+[]{}\\|;:'\",<.>/?".ToCharArray();

        #endregion

        #region Ctor

        public RemoteServer()
        {
            using (var hashProvider = new MD5CryptoServiceProvider())
            {
                var password = Encoding.Unicode.GetBytes(GeneratePassword());
                var hashValue = hashProvider.ComputeHash(password);
                Users.TryAdd(ServerConfig.ReadConfiguration().UserName, Encoding.Unicode.GetString(hashValue));
            }
        }

        #endregion

        #region Properties

        private static RemoteServer Instance
        {
            get { return _instance.Value; }
        }

        private ConcurrentDictionary<string, string> Users
        {
            get
            {
                return _users;
            }
        }

        private static int ResponseDelay
        {
            get
            {
                var config = ServerConfig.ReadConfiguration();
                lock (_syncRoot)
                {
                    return _random.Next(config.MinResponseDelay, config.MaxResponseDelay);
                }
            }
        }

        #endregion

        #region Public Methods

        public static void Login(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user", ServerStrings.ErrorUserIsNull);
            Thread.Sleep(ResponseDelay);
            string password;
            if (!Instance.Users.TryGetValue(user.Username, out password))
            {
                throw new AuthenticationException(ServerStrings.ErrorInvalidUserName);
            }
            if (password != user.Password)
            {
                throw new AuthenticationException(ServerStrings.ErrorInvalidPassword);
            }
        }

        #endregion

        #region Private Methods

        private string GeneratePassword()
        {
            string pwd = null;
            ServerConfig config = ServerConfig.ReadConfiguration();
            pwd = new String(Enumerable.Range(1, config.MaxPasswordLength)
                .Select(_ => _passwordChars[_random.Next(_passwordChars.Length)]).ToArray());
            return pwd;
        }

        #endregion

    }
}
