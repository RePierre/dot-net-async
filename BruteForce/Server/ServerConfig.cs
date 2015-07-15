using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetAsyncExamples.BruteForce.Server
{
    public class ServerConfig : ConfigurationSection
    {
        #region Constants

        private const string UserNameProperty = "userName";
        private const string MaxPasswordLengthProperty = "maxPasswordLength";
        private const string MinResponseDelayProperty = "minResponseDelay";
        private const string MaxResponseDelayProperty = "maxResponseDelay";

        #endregion

        #region Properties

        [ConfigurationProperty(UserNameProperty)]
        public string UserName
        {
            get { return (string)base[UserNameProperty]; }
            set { base[UserNameProperty] = value; }
        }

        [ConfigurationProperty(MinResponseDelayProperty)]
        public int MinResponseDelay
        {
            get { return (int)base[MinResponseDelayProperty]; }
            set { base[MinResponseDelayProperty] = value; }
        }

        [ConfigurationProperty(MaxResponseDelayProperty)]
        public int MaxResponseDelay
        {
            get { return (int)base[MaxResponseDelayProperty]; }
            set { base[MaxResponseDelayProperty] = value; }
        }

        [ConfigurationProperty(MaxPasswordLengthProperty)]
        public int MaxPasswordLength
        {
            get { return (int)base[MaxPasswordLengthProperty]; }
            set { base[MaxPasswordLengthProperty] = value; }
        }

        #endregion

        #region Public Methods

        public static ServerConfig ReadConfiguration()
        {
            var section = ConfigurationManager.GetSection("ServerConfiguration");
            return section as ServerConfig;
        }

        #endregion
    }
}
