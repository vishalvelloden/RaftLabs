using MediatR;
using Proxy.Services.Objects.ExternalUserAPI.DTO;
using RaftLabs.Application.Proxy;
using RaftLabs.Application.Queries;

namespace RaftLabs.Application.Handlers.Users.Queries.GetAllUser
{
    /// <summary>
    /// The get all user query handler.
    /// </summary>
    public class GetAllUserQueryHandler
        : IRequestHandler<GetAllUserQuery, GetPagedResponseBase<GetAllUserQueryResponse>>
    {
        /// <summary>
        /// The client used to interact with the external user service.
        /// </summary>
        private readonly IExternalUserClient externalUserClient;

        /// <summary>
        /// Represents the cache service used for storing and retrieving cached data.
        /// </summary>
        private readonly ICacheService cacheService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllUserQueryHandler"/> class.
        /// </summary>
        /// <param name="externalUserClient">The external user client.</param>
        /// <param name="cacheService">The cache service.</param>
        public GetAllUserQueryHandler(
            IExternalUserClient externalUserClient,
            ICacheService cacheService)
        {
            this.externalUserClient = externalUserClient;
            this.cacheService = cacheService;
        }

        /// <summary>
        /// Handles the.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task.</returns>
        public async Task<GetPagedResponseBase<GetAllUserQueryResponse>> Handle(
            GetAllUserQuery request,
            CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            const string cacheKey = "all_users";
            var allUsers = await cacheService.GetAsync<IEnumerable<UserDto>>(cacheKey);

            if (allUsers == null)
            {
                allUsers = await externalUserClient.GetAllUsersAsync();
                await cacheService.SetAsync(cacheKey, allUsers);
            }

            var totalCount = allUsers.Count();
            var pageIndex = request.Index;
            var pageSize = request.Size;
            var skip = pageIndex * pageSize;

            var pagedUsers = allUsers
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            return new GetPagedResponseBase<GetAllUserQueryResponse>
            {
                From = skip + 1,
                Index = pageIndex,
                Size = pageSize,
                Count = totalCount,
                Records = pagedUsers.Select(user => new GetAllUserQueryResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.First_name,
                    LastName = user.Last_name,
                    Avatar = user.Avatar,
                })
                .ToList(),
            };
        }
    }
}
