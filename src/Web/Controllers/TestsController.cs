
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using MyWebApi.Infrastructure.Data;

namespace MyWebApi.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TestsController : ApiControllerBase
{
    private readonly THUCHANHNET8_DAFContext _context;

    public TestsController(THUCHANHNET8_DAFContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult> TestApi()
    {
        var sender = await Mediator.Send(new GetWeatherForecastsQuery());
        return Ok(sender);
    }
    [HttpGet("test2")]
    public ActionResult TestApi2()
    {

        var rooms = _context.Hotels.ToList();
        return Ok(rooms);
    }

}


