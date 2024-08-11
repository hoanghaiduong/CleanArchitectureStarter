
using Microsoft.AspNetCore.Mvc;

namespace MyWebApi.Web.Controllers;
[Route("api/[controller]")]
[Produces("application/json")]
[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
