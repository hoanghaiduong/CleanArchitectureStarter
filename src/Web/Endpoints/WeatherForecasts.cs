using Microsoft.AspNetCore.Authorization;
using MyWebApi.Application.WeatherForecasts.Queries.GetWeatherForecasts;

namespace MyWebApi.Web.Endpoints;

public class WeatherForecasts : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup("kaka")
            .RequireAuthorization()
            .MapGet(GetWeatherForecasts);
    }

    // [AllowAnonymous]
    public async Task<IEnumerable<WeatherForecast>> GetWeatherForecasts(ISender sender)
    {
        return await sender.Send(new GetWeatherForecastsQuery());
    }
}
