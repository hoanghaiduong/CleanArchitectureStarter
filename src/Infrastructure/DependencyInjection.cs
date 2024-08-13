using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using MyWebApi.Application.Common.Interfaces;
using MyWebApi.Infrastructure.Data;
using MyWebApi.Infrastructure.Data.Interceptors;


namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("DefaultConnectionMain");
        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");
        Console.WriteLine(connectionString);
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        //services.AddDbContext<ApplicationDbContext>((sp, options) =>
        //{
        //    options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

        //    options.UseSqlServer(connectionString);
        //});

        //services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddDbContext<THUCHANHNET8_DAFContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<THUCHANHNET8_DAFContext>());
        services.AddScoped<ApplicationDbContextInitialiser>();


        //services.AddAuthorizationBuilder();

        services.AddSingleton(TimeProvider.System);


        return services;
    }
}
