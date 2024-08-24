
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MyWebApi.Infrastructure.Data;
using MyWebApi.Infrastructure.Identity;
using MyWebApi.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace MyWebApi.Web.Infrastructure
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityHandlerAndStore(this IServiceCollection services)
        {
            services
            .AddIdentityCore<ApplicationUser>()
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddApiEndpoints();

            return services;
        }
        public static IServiceCollection ConfigureIdentityOptions(this IServiceCollection services)
        {

            services.Configure<IdentityOptions>(ops =>
            {

                ops.Password.RequireDigit = false;
                ops.Password.RequireUppercase = false;
                ops.Password.RequireLowercase = false;
                ops.User.RequireUniqueEmail = true;
            });
            return services;
        }

        public static IServiceCollection AddIdentityAuth(this IServiceCollection services, IConfiguration config)
        {
            var accessTokenSecret = config.GetValue<string>("AppSettings:JWT_ACCESS_TOKEN_SECRET");
            var refreshTokenSecret = config.GetValue<string>("AppSettings:JWT_REFRESH_TOKEN_SECRET");

            Guard.Against.Null(accessTokenSecret, message: "Jwt Secret 'JWT_ACCESS_TOKEN_SECRET' not found.");
            Guard.Against.Null(refreshTokenSecret, message: "Jwt Secret 'JWT_REFRESH_TOKEN_SECRET' not found.");
        
            //use Identity Default Token
            // services.AddAuthentication()
            //    .AddBearerToken(IdentityConstants.BearerScheme);


            //use Identity Jwt Token Authentication Custom
            services
            .AddAuthentication(x =>
            {
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, y =>
            {
                y.SaveToken = false;
                y.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessTokenSecret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            })
            .AddJwtBearer("RefreshToken", y =>
            {
                y.SaveToken = false;
                y.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(refreshTokenSecret)),
                    ValidateIssuer = false,

                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme, "RefreshToken")
            .RequireAuthenticatedUser()
            .Build();
                options.AddPolicy(Policies.Manager, policy =>
                {
                    //policy.RequireAssertion(handler: context => Boolean.Parse(context.User.Claims.First(u => u.Type == "EmailVerified").Value));
                    policy.RequireRole(Roles.Staff);
                });
                options.AddPolicy(Policies.Receptionist, policy =>
                {
                 
                    policy.RequireRole(Roles.Staff);
                });
            });



            return services;
        }
        public static WebApplication AddIdentityMiddleware(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }
    }
}
