using System;
using System.Collections.Generic;
using System.Text;

namespace StooqWebsite
{
    public class StooqWebsiteException : Exception
    {
        public StooqWebsiteException()
        {
        }

        public StooqWebsiteException(string message)
            : base(message)
        {
        }

        public StooqWebsiteException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
