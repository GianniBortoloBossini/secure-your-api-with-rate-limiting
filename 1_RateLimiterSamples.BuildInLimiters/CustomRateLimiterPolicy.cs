using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace RateLimiterSamples.BuildInLimiters;

public class CustomRateLimiterPolicy : IRateLimiterPolicy<string>
{
    public RateLimitPartition<string> GetPartition(HttpContext httpContext)
    {
        string partitionKey = httpContext.User.Identity?.Name ?? "free";
        Console.WriteLine(partitionKey);
        if (partitionKey.Contains("admin"))
            return RateLimitPartition.GetNoLimiter(partitionKey);
        else 
            return RateLimitPartition.GetFixedWindowLimiter(partitionKey,
                    partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = partitionKey.Contains("free") ? 1 : 5,
                        Window = TimeSpan.FromSeconds(5),
                    });
    }

    public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected { get; } =
        (context, _) =>
        {
            context.HttpContext.Response.StatusCode = 418; // I'm a Teapot
            return new ValueTask();
        };
}
