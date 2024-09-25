
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

using MyWebApi.Web.Services;



public partial class Program
{
    private static async Task Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
        builder.Services.Configure<JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        // Add services to the container.
        builder.Services.AddSwaggerExplorer();
        builder.Services.InjectDBContext(builder.Configuration);
        builder.Services.AddKeyVaultIfConfigured(builder.Configuration);
        builder.Services.AddApplicationServices();
        builder.Services.AddIdentityHandlerAndStore().ConfigureIdentityOptions().AddIdentityAuth(builder.Configuration);
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddWebServices();
        // Console.WriteLine(builder.Configuration["Authentication:Google:ClientId"]);
        // Console.WriteLine(builder.Configuration["Authentication:Google:ClientSecret"]);
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        await app.ConfigureSwaggerExplorer();
        app.AddIdentityMiddleware();
        app.AddMiddleware();
        app.UseHealthChecks("/health");
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseExceptionHandler(new ExceptionHandlerOptions()
        {
                AllowStatusCode404Response = true, // important!
                ExceptionHandlingPath = "/error"
        });

        //Nếu sử dụng UI
        app.MapRazorPages();
        app.MapEndpoints();
        app.MapControllers();

        app.Run();

    }
}

public partial class Program { }
