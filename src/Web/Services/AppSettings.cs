namespace MyWebApi.Web.Services
{
    public class AppSettings
    {
        public string? JWT_ACCESS_TOKEN_SECRET { get; set; }
        public string? JWT_REFRESH_TOKEN_SECRET { get; set; }
    }
}