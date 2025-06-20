namespace Proxy.Services.Objects.Configuration
{
    /// <summary>
    /// The cache settings.
    /// </summary>
    public class CacheSettings
    {
        /// <summary>
        /// Gets or sets the default cache seconds.
        /// </summary>
        public int DefaultCacheSeconds { get; set; } = 60; // default to 60 seconds
    }
}
