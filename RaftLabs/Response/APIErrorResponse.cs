namespace RaftLabs.API.Response
{
    /// <summary>
    /// The a p i error response.
    /// </summary>
    public class APIErrorResponse
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [validation failed].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [validation failed]; otherwise, <c>false</c>.
        /// </value>
        public bool ValidationFailed { get; set; }

        /// <summary>
        /// Gets or Sets the errors.
        /// </summary>
        public IEnumerable<string>? Errors { get; set; }
    }
}
