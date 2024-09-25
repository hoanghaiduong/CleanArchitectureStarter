namespace MyWebApi.Web.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                // Handle non-success status codes as errors
                if (context.Response.StatusCode >= 400 && context.Response.StatusCode < 600)
                {
                    await HandleErrorAsync(context, context.Response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                // Handle all exceptions and default to 500
                await HandleErrorAsync(context, StatusCodes.Status500InternalServerError, ex);
            }
        }

        private Task HandleErrorAsync(HttpContext context, int statusCode, Exception? exception = null)
        {
            // Ensure the response body is not written multiple times
            if (context.Response.HasStarted)
            {
                // Log if needed
                return Task.CompletedTask;
            }

            context.Response.Clear(); // Clear the existing response
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var result = new
            {
                StatusCode = statusCode,
                Message = statusCode switch
                {
                    StatusCodes.Status400BadRequest => "Bad Request",
                    StatusCodes.Status401Unauthorized => "Unauthorized",
                    StatusCodes.Status403Forbidden => "Forbidden",
                    StatusCodes.Status404NotFound => "Resource Not Found",
                    StatusCodes.Status500InternalServerError => "Internal Server Error",
                    _ => "An error occurred"
                },
                // Detailed = exception?.Message // Optional: Include stack trace or exception details in development
            };

            return context.Response.WriteAsJsonAsync(result);
        }
    }
}
