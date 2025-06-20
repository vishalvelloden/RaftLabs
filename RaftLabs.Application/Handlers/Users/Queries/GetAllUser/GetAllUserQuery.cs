using MediatR;
using RaftLabs.Application.Queries;

namespace RaftLabs.Application.Handlers.Users.Queries.GetAllUser
{
    /// <summary>
    /// The get all user query.
    /// </summary>
    public class GetAllUserQuery
        : GetPagedRequestBase, IRequest<GetPagedResponseBase<GetAllUserQueryResponse>>
    {
    }
}
