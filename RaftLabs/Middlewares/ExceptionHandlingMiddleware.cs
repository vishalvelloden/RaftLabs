using System.Net;
using Newtonsoft.Json;

namespace RaftLabs.API.Middlewares
{
    /// <summary>
    /// The exception handling middleware.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        /// <summary>
        /// The next.
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public ExceptionHandlingMiddleware(
            RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Invokes the async.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A Task.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            // Enable buffering for request reading.
            context.Request.EnableBuffering();

            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                await this.HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handles the exception async.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>A Task.</returns>
        private async Task HandleExceptionAsync(
           HttpContext context,
           Exception exception)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(exception);

            context.Response.ContentType = "application/json";
            int statusCode;
            string message;
            string? details = null;

            switch (exception)
            {
                // Network issues, timeouts, and HTTP errors
                case HttpRequestException httpEx:
                    statusCode = (int)(httpEx.StatusCode ?? HttpStatusCode.BadGateway);
                    message = "A problem occurred while communicating with the external service.";
                    details = httpEx.Message;
                    break;

                // De serialization errors
                case Newtonsoft.Json.JsonException jsonEx:
                    statusCode = (int)HttpStatusCode.BadGateway;
                    message = "Failed to process data from the external service.";
                    details = jsonEx.Message;
                    break;

                // Not found (e.g., 404 from external API)
                case ApplicationException appEx:
                    statusCode = (int)HttpStatusCode.NotFound;
                    message = "The requested resource was not found.";
                    details = appEx.Message;
                    break;

                // General/unhandled errors
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    message = "An unexpected error occurred.";
                    details = exception.Message;
                    break;
            }

            context.Response.StatusCode = statusCode;

            var errorResponse = new
            {
                error = message,
                details
            };

            var json = JsonConvert.SerializeObject(errorResponse);

            await context.Response.WriteAsync(json);
        }
    }
}
