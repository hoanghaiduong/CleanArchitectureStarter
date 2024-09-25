

using MyWebApi.Web.Middlewares;

namespace MyWebApi.Web.Infrastructure
{
    public static class MiddlewareExtension
    {
        public static WebApplication AddMiddleware(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            return app;
        }
    }
}