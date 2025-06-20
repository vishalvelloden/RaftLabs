using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Proxy.Services.Base;
using Proxy.Services.Objects.ExternalUserAPI.DTO;
using RaftLabs.Application.Proxy;
using Shared.Common.AppSettings;
using System.Text;

namespace Proxy.Services.Clients
{
    /// <summary>
    /// The external user client.
    /// </summary>
    public class ExternalUserClient
        : ServiceClientBase, IExternalUserClient
    {
        /// <summary>
        /// The base URL used for constructing API requests.
        /// </summary>
        /// <remarks>This field holds the root URL for the API and is intended to be used internally for
        /// building endpoint-specific URLs. It is a read-only field and cannot be modified after
        /// initialization.</remarks>
        private readonly string baseUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalUserClient"/> class.
        /// </summary>
        /// <param name="httpClient">The http client.</param>
        public ExternalUserClient(
            HttpClient httpClient,
             IOptions<ExternalUserSettings> settings)
            : base(httpClient)
        {
            baseUrl = settings.Value.BaseURL ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <summary>
        /// Gets the all users async.
        /// </summary>
        /// <returns>A Task.</returns>
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var allUsers = new List<UserDto>();
            int currentPage = 1;
            int totalPages = 1;

            do
            {
                var response = await this.SendAsync(
                    HttpMethod.Get,
                    $"{baseUrl}/users?page={currentPage}",
                    cancellationToken: CancellationToken.None);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Status Code: {(int)response.StatusCode}, Error: {error}");
                }

                var stream = await response.Content.ReadAsStreamAsync();
                using var reader = new StreamReader(stream, Encoding.UTF8);
                var json = await reader.ReadToEndAsync();

                var result = JsonConvert.DeserializeObject<PagedUserResponseDto>(json);

                if (result?.Data != null)
                {
                    allUsers.AddRange(result.Data);
                }

                totalPages = result?.Total_Pages ?? 1;
                currentPage++;

            }
            while (currentPage <= totalPages);

            return allUsers;
        }

        /// <summary>
        /// Gets the user by id async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public async Task<UserDto> GetUserByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var response = await this.SendAsync(
                    HttpMethod.Get,
                    $"{baseUrl}/users/{id}",
                    cancellationToken: CancellationToken.None);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Status Code: {(int)response.StatusCode}, Error: {error}");
            }

            var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream, Encoding.UTF8);
            var json = await reader.ReadToEndAsync();

            var jObject = JObject.Parse(json);
            var userJson = jObject["data"]?.ToString();

            if (string.IsNullOrEmpty(userJson))
            {
                throw new InvalidOperationException("The 'data' field in the response is null or empty.");
            }

            var user = JsonConvert.DeserializeObject<UserDto>(userJson);

            if (user == null)
            {
                throw new InvalidOperationException("The response content could not be deserialized into a UserDto.");
            }

            return user;
        }
    }
}
