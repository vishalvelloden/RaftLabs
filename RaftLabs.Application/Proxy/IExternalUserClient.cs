using Proxy.Services.Objects.ExternalUserAPI.DTO;
namespace RaftLabs.Application.Proxy
{
    /// <summary>
    /// The external user client.
    /// </summary>
    public interface IExternalUserClient
    {
        /// <summary>
        /// Gets the all users async.
        /// </summary>
        /// <returns>A Task.</returns>
        Task<IEnumerable<UserDto>> GetAllUsersAsync();

        /// <summary>
        /// Gets the user by id async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        Task<UserDto> GetUserByIdAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}
