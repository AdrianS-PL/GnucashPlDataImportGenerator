using System;
using System.Collections.Generic;
using System.Text;

namespace BossaWebsite
{
    public class BossaWebsiteException : Exception
    {
        public BossaWebsiteException()
        {
        }

        public BossaWebsiteException(string message)
            : base(message)
        {
        }

        public BossaWebsiteException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
