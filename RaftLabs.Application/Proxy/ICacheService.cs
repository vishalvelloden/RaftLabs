namespace RaftLabs.Application.Proxy
{
    /// <summary>
    /// The cache service.
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Gets the async.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A Task.</returns>
        Task<T?> GetAsync<T>(string key);

        /// <summary>
        /// Sets the async.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expiration">The expiration.</param>
        /// <returns>A Task.</returns>
        Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);

        /// <summary>
        /// Removes the.
        /// </summary>
        /// <param name="key">The key.</param>
        void Remove(string key);
    }
}
