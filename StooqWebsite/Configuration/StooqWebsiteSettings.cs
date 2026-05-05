using System.Diagnostics.CodeAnalysis;

namespace StooqWebsite.Configuration
{
    [ExcludeFromCodeCoverage]
    public class StooqWebsiteSettings
    {
        public string ApiKey { get; set; } = string.Empty;
    }
}
