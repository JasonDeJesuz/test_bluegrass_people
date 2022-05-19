using Microsoft.AspNetCore.Mvc;

namespace Bluegrass.Core.Controllers;

[ApiController]
[Route("[controller]")]
public class HelperController : ControllerBase
{

    private readonly ILogger<WeatherForecastController> _logger;

    public HelperController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    
}