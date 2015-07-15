using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetAsyncExamples.BruteForce.Domain.Entities
{
    class User
    {
        #region Properties

        public string Username { get; set; }

        public string Password { get; set; }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return Username;
        }

        #endregion
    }
}
