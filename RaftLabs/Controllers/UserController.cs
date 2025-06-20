using MediatR;
using Microsoft.AspNetCore.Mvc;
using RaftLabs.Application.Handlers.Users.Queries.GetAllUser;
using RaftLabs.Application.Handlers.Users.Queries.GetUserById;

namespace RaftLabs.Controllers
{
    /// <summary>
    /// The user controller.
    /// </summary>
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {

        /// <summary>
        /// The mediator instance for sending requests and commands.
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public UserController(
            IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Gets the all async.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>A Task.</returns>
        [HttpGet("all")]
        public async Task<ActionResult> GetAllAsync(
            [FromQuery] GetAllUserQuery query)
        {
            ArgumentNullException.ThrowIfNull(query);

            var response = await this.mediator.Send(query, CancellationToken.None);

            return this.Ok(response);
        }
        /// <summary>
        /// Gets a user by id.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <returns>A Task.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult> GetByIdAsync(
            int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid user id.");
            }

            var response = await this.mediator.Send(
                new GetUserByIdQuery
                {
                    Id = id
                },
                CancellationToken.None);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

    }
}
