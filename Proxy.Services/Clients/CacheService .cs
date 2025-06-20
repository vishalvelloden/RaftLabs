using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Proxy.Services.Objects.Configuration;
using RaftLabs.Application.Proxy;

namespace Proxy.Services.Clients
{
    /// <summary>
    /// The cache service.
    /// </summary>
    public class CacheService : ICacheService
    {
        /// <summary>
        /// Represents a memory cache used for storing and retrieving data in memory.
        /// </summary>
        /// <remarks>This field is intended for internal use to manage cached data efficiently. It
        /// provides a mechanism to temporarily store data in memory to improve performance by reducing the need for
        /// repeated computations or external data retrieval.</remarks>
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// Represents the configuration settings for the cache.
        /// </summary>
        /// <remarks>This field is read-only and is used to store the cache settings that influence the
        /// behavior of caching operations. It is initialized during the construction of the containing class and cannot
        /// be modified afterward.</remarks>
        private readonly CacheSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheService"/> class.
        /// </summary>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="settings">The settings.</param>
        public CacheService(
            IMemoryCache memoryCache,
            IOptions<CacheSettings> settings)
        {
            _memoryCache = memoryCache;
            _settings = settings.Value;
        }

        /// <summary>
        /// Gets the async.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A Task.</returns>
        public async Task<T?> GetAsync<T>(string key)
        {
            if (_memoryCache.TryGetValue(key, out var value) && value is T typedValue)
            {
                return await Task.FromResult(typedValue);
            }
            return await Task.FromResult(default(T));
        }

        /// <summary>
        /// Removes the.
        /// </summary>
        /// <param name="key">The key.</param>
        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        /// <summary>
        /// Sets the async.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiration">The expiration.</param>
        /// <returns>A Task.</returns>
        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)

        {
            var options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromSeconds(_settings.DefaultCacheSeconds)

            };

            _memoryCache.Set(key, value, options);
            await Task.CompletedTask;
        }
    }
}
