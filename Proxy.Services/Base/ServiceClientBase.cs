using System.Net.Http.Headers;

namespace Proxy.Services.Base
{
    /// <summary>
    /// The service client base.
    /// </summary>
    public class ServiceClientBase
    {

        /// <summary>
        /// The HTTP client used for sending HTTP requests.
        /// </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceClientBase"/> class.
        /// </summary>
        /// <param name="httpClient">The http client.</param>
        public ServiceClientBase(
            HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Sends the async.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="requestUrl">The request url.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public virtual async Task<HttpResponseMessage> SendAsync(
            HttpMethod method,
            string requestUrl,
            CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(requestUrl);

                // Add JSON headers.
                var mediaTypeHeader = new MediaTypeWithQualityHeaderValue("application/json");
                this.httpClient.DefaultRequestHeaders.Accept.Add(mediaTypeHeader);

                var requestMessage = new HttpRequestMessage(method, requestUrl);
                requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                const int maxRetries = 3;
                int retryCount = 0;
                while (true)
                {
                    try
                    {
                        // Send the request to server.
                        return await this.httpClient.SendAsync(requestMessage, cancellationToken);
                    }
                    catch (HttpRequestException) when (retryCount < maxRetries)
                    {
                        retryCount++;
                        await Task.Delay(500, cancellationToken);
                        // Re-create the request message for retry
                        requestMessage = new HttpRequestMessage(method, requestUrl);
                        requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
