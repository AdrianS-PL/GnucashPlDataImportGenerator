using System;
using System.Diagnostics.CodeAnalysis;

namespace StooqWebsite;

[ExcludeFromCodeCoverage]
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
