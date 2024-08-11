
using Microsoft.AspNetCore.Mvc;

using MyWebApi.Application.WeatherForecasts.Queries.GetWeatherForecasts;

namespace MyWebApi.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TestsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult> TestApi()
    {
        var sender = await Mediator.Send(new GetWeatherForecastsQuery());
        return Ok(sender);
    }

}
