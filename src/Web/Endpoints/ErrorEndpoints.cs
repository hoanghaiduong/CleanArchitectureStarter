using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;

namespace MyWebApi.Web.Endpoints
{
    public class ErrorEndpoints : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup("error")
               .MapGet(GetHandleError); // Maps the error handling endpoint
        }
        [AllowAnonymous]
        private IResult GetHandleError(HttpContext context)
        {
            var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionFeature?.Error;

            var statusCode = context.Response.StatusCode;
            var result = new
            {
                StatusCode = statusCode,
                Message = exception?.Message ?? "An unexpected error occurred.",
                Detailed = exception?.StackTrace // Optional: Include stack trace for detailed error reporting
            };

            // Here, you can log the exception or do other necessary actions

            return Results.Json(result);
        }
    }
}
