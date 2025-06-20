namespace RaftLabs.Application.Handlers.Users.Queries.GetAllUser
{
    /// <summary>
    /// The get all user query response.
    /// </summary>
    public class GetAllUserQueryResponse
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
        /// Gets or sets the first_ name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last_ name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the avatar.
        /// </summary>
        public string Avatar { get; set; }
    }
}
