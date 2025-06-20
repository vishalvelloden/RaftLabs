using MediatR;

namespace RaftLabs.Application.Handlers.Users.Queries.GetUserById
{
    /// <summary>
    /// The get user by id query.
    /// </summary>
    public class GetUserByIdQuery
        : IRequest<GetUserByIdQueryResponse>
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }
    }
}
