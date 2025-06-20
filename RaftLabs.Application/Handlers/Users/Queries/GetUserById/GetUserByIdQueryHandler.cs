using MediatR;
using Proxy.Services.Objects.ExternalUserAPI.DTO;
using RaftLabs.Application.Proxy;

namespace RaftLabs.Application.Handlers.Users.Queries.GetUserById
{
    /// <summary>
    /// Handles the GetUserByIdQuery and returns user details by user id.
    /// </summary>
    public class GetUserByIdQueryHandler
        : IRequestHandler<GetUserByIdQuery, GetUserByIdQueryResponse>
    {
        /// <summary>
        /// The external user client used to fetch user data.
        /// </summary>
        private readonly IExternalUserClient externalUserClient;

        /// <summary>
        /// Represents the cache service used for storing and retrieving cached data.
        /// </summary>
        private readonly ICacheService cacheService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserByIdQueryHandler"/> class.
        /// </summary>
        /// <param name="externalUserClient">The external user client.</param>
        /// <param name="cacheService">The cache service.</param>
        public GetUserByIdQueryHandler(
            IExternalUserClient externalUserClient,
            ICacheService cacheService)
        {
            this.externalUserClient = externalUserClient;
            this.cacheService = cacheService;
        }

        /// <summary>
        /// Handles the GetUserByIdQuery request.
        /// </summary>
        /// <param name="request">The request containing the user id.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="GetUserByIdQueryResponse"/> containing user details.</returns>
        public async Task<GetUserByIdQueryResponse> Handle(
            GetUserByIdQuery request,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var cacheKey = $"user:{request.Id}";
            var user = await cacheService.GetAsync<UserDto>(cacheKey);

            if (user == null)
            {
                user = await externalUserClient.GetUserByIdAsync(request.Id);

                await cacheService.SetAsync(cacheKey, user);
            }

            return new GetUserByIdQueryResponse
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.First_name,
                LastName = user.Last_name,
                Avatar = user.Avatar,
            };
        }
    }
}
