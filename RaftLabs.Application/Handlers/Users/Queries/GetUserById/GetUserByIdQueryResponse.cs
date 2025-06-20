namespace RaftLabs.Application.Handlers.Users.Queries.GetUserById
{
    /// <summary>
    /// The get user by id query response.
    /// </summary>
    public class GetUserByIdQueryResponse
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the avatar.
        /// </summary>
        public string Avatar { get; set; }
    }
}
