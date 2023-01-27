using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace RateLimiterSamples.BuildInLimiters.Mvc.Controllers;
[ApiController]
[EnableRateLimiting("ApiLimit")]
[Route("[controller]")]
public class GreetingController : ControllerBase
{
    [HttpGet("health")]
    [DisableRateLimiting]
    public IActionResult Health()
    {
        return Ok();
    }

    [HttpGet("hello")]
    [EnableRateLimiting("HelloLimit")]
    public IActionResult Hello()
    {
        return Ok();
    }

    [HttpGet("ciao")]
    public IActionResult Ciao()
    {
        return Ok();
    }
}
