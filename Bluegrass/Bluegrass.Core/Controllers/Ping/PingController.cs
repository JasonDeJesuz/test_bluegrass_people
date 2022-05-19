using Microsoft.AspNetCore.Mvc;

namespace Bluegrass.Core.Controllers.Ping;

[ApiController]
[Route("api/[controller]")]
public class PingController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<PingController> _logger;

    public PingController(ILogger<PingController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok( new { StatusCode = 200 } );
    }
}