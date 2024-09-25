
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MyWebApi.Infrastructure.Data;

namespace MyWebApi.Web.Infrastructure
{
    public static class EFCoreExtensions
    {
        public static IServiceCollection InjectDBContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnectionMain");
            Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnectionMain' not found.");
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
            });
            return services;
        }
    }
}