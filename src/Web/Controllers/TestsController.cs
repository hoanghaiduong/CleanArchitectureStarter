
using Microsoft.AspNetCore.Mvc;
using MyWebApi.Application.Common.Interfaces;
using MyWebApi.Application.WeatherForecasts.Queries.GetWeatherForecasts;

namespace MyWebApi.Web.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TestsController : ApiControllerBase
{
    private readonly IApplicationDbContext? _context;

    public TestsController(IApplicationDbContext? context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult TestApi()
    {
        var hotels = _context?.Hotels.ToList();

        return Ok(new
        {
            hotels
        });
    }
}
