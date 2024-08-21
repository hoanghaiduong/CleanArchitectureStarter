using MyWebApi.Infrastructure.Identity;
using MyWebApi.Web.Services;

public partial class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
        builder.Services.Configure<SMSoptions>(builder.Configuration.GetSection("SMSoptions"));
        // Add services to the container.
        builder.Services.AddSwaggerExplorer();
        builder.Services.InjectDBContext(builder.Configuration);
        builder.Services.AddKeyVaultIfConfigured(builder.Configuration);
        builder.Services.AddApplicationServices();
        builder.Services.AddIdentityHandlerAndStore().ConfigureIdentityOptions().AddIdentityAuth(builder.Configuration);
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddWebServices();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        await app.ConfigureSwaggerExplorer();
        app.AddIdentityMiddleware();

        app.UseHealthChecks("/health");
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseExceptionHandler(options => { });


        //Nếu sử dụng UI
        app.MapRazorPages();
        app.MapEndpoints();
        app.MapControllers();

        app.Run();
    }
}

public partial class Program { }
