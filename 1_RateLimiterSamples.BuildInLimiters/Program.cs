using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Primitives;
using RateLimiterSamples.BuildInLimiters;
using System.Globalization;
using System.Threading.RateLimiting;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

// 1 - CONCURRENCY LIMITER

#region 1 - ConcurrencyLimiter 

// builder.Services.AddRateLimiter(options =>
// {
//     options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
//         RateLimitPartition.GetConcurrencyLimiter(
//             partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
//             factory: partition => new ConcurrencyLimiterOptions
//             {
//                 PermitLimit = 1
//             }));
// });

#endregion

#region 2 - ConcurrencyLimiter, statuscode = 429

// builder.Services.AddRateLimiter(options =>
// {
//    options.RejectionStatusCode = 429;
//    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
//        RateLimitPartition.GetConcurrencyLimiter(
//            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
//            factory: partition => new ConcurrencyLimiterOptions
//            {
//                PermitLimit = 1
//            }));
// });

#endregion

#region 3 - ConcurrencyLimiter con coda

// builder.Services.AddRateLimiter(options =>
// {
//    options.RejectionStatusCode = 429;
//    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
//        RateLimitPartition.GetConcurrencyLimiter(
//            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
//            factory: partition => new ConcurrencyLimiterOptions
//            {
//                PermitLimit = 1,
//                QueueLimit = 2,
//                QueueProcessingOrder = QueueProcessingOrder.OldestFirst
//            }));
// });

#endregion


// 2 - FIXED-WINDOW LIMITER

#region 4 - FixedWindowLimiter 

// builder.Services.AddRateLimiter(options =>
// {
//    options.RejectionStatusCode = 429;
//    options.GlobalLimiter =
//          PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
//              RateLimitPartition.GetFixedWindowLimiter(
//                  partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
//                  factory: partition => new FixedWindowRateLimiterOptions
//                {
//                    AutoReplenishment = true,
//                    PermitLimit = 2,
//                    Window = TimeSpan.FromSeconds(5)
//                }));
// });

#endregion

#region 5 - FixedWindowLimiter con retry-after

// builder.Services.AddRateLimiter(options =>
// {
//    options.RejectionStatusCode = 429;
//    options.GlobalLimiter =
//        PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
//            RateLimitPartition.GetFixedWindowLimiter(
//                partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
//                factory: partition => new FixedWindowRateLimiterOptions
//                {
//                    AutoReplenishment = true,
//                    PermitLimit = 2,
//                    Window = TimeSpan.FromSeconds(5)
//                }));
//    options.OnRejected = async (context, token) =>
//    {
//        Console.WriteLine("Request refused!");
//        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
//        {
//            context.HttpContext.Response.Headers["Retry-After"] = retryAfter.TotalSeconds.ToString(CultureInfo.InvariantCulture);
//            await context.HttpContext.Response.WriteAsync(
//                $"Too many requests. Please try again after {retryAfter.TotalSeconds} second(s). " +
//                $"Read more about our rate limits at https://example.org/docs/ratelimiting.", cancellationToken: token);
//        }
//        else
//        {
//            await context.HttpContext.Response.WriteAsync(
//                "Too many requests. Please try again later. " +
//                "Read more about our rate limits at https://example.org/docs/ratelimiting.", cancellationToken: token);
//        }
//    };
// });

#endregion

// 3 - SLIDING-WINDOW LIMITER

#region 6 - SlidingWindowLimiter 

// builder.Services.AddRateLimiter(options =>
// {
//     options.RejectionStatusCode = 429;
//     options.GlobalLimiter =
//          PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
//              RateLimitPartition.GetSlidingWindowLimiter(
//                  partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
//                  factory: partition => new SlidingWindowRateLimiterOptions
//                {
//                    AutoReplenishment = true,
//                    PermitLimit = 5,
//                    Window = TimeSpan.FromSeconds(15),
//                    SegmentsPerWindow = 3
//                }));
// });

#endregion

// 4 - TOKEN-BUCKET LIMITER

#region 7 - TokenBucketLimiter 

// builder.Services.AddRateLimiter(options =>
// {
//    options.RejectionStatusCode = 429;
//    options.GlobalLimiter =
//        PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
//            RateLimitPartition.GetTokenBucketLimiter(
//                partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
//                factory: partition => new TokenBucketRateLimiterOptions
//                {
//                    AutoReplenishment = true,
//                    TokenLimit = 5,
//                    TokensPerPeriod = 2,
//                    ReplenishmentPeriod = TimeSpan.FromSeconds(10)
//                }));
// });

#endregion

// 5 - CUSTOM POLICY E ABILITAZIONE/DISABILITAZIONE SELETTIVA PER ENDPOINTS E GRUPPI

#region 8 - Disabilitazione selettiva risorse dal rate limit

// builder.Services.AddRateLimiter(options =>
// {
//    options.RejectionStatusCode = 429;
//    options.GlobalLimiter =
//        PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
//            RateLimitPartition.GetFixedWindowLimiter(
//                partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
//                factory: partition => new FixedWindowRateLimiterOptions
//                {
//                    AutoReplenishment = true,
//                    PermitLimit = 2,
//                    Window = TimeSpan.FromSeconds(5)
//                }));
// });

#endregion

#region 9 - Custom policy e Abilitazione/Disabilitazione selettiva per endpoint e gruppi

// builder.Services.AddRateLimiter(options =>
// {
//    options.AddPolicy<string, CustomRateLimiterPolicy>("professional-api");
// });

#endregion

#region 10 - Chain multiple rate limiting

// builder.Services.AddRateLimiter(options =>
// {
//    options.RejectionStatusCode = 429;
//    options.GlobalLimiter =
//        PartitionedRateLimiter.CreateChained(
//            PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
//            RateLimitPartition.GetFixedWindowLimiter(httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(), 
//            partition =>
//                new FixedWindowRateLimiterOptions
//                {
//                    AutoReplenishment = true,
//                    PermitLimit = 10,
//                    Window = TimeSpan.FromSeconds(10)
//                })),
//            PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
//                RateLimitPartition.GetFixedWindowLimiter(httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(), 
//                partition =>
//                    new FixedWindowRateLimiterOptions
//                    {
//                        AutoReplenishment = true,
//                        PermitLimit = 20,
//                        Window = TimeSpan.FromSeconds(30)
//                    }))
//            );
// });

#endregion


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) => {
    context.Response.Headers.Add("x-user-identifier", context.User.Identity?.Name); 
    await next();
});

app.UseRateLimiter();

app.MapGet("/api/hello", () => {
    Console.WriteLine("Request received!");
    Thread.Sleep(100);
    return Results.Ok();
});

app.MapGet("/api/buy", () => {
    Thread.Sleep(100);
    return Results.Ok();
}).RequireRateLimiting("professional-api");

app.MapGet("/api/health", () => {
    Thread.Sleep(100);
    return Results.Ok();
}); //.DisableRateLimiting();

var statisticsGroup = app.MapGroup("/api/statistics").RequireRateLimiting("professional-api");
statisticsGroup.MapGet("/users", () => Results.Ok(new { Count = 34 }));
statisticsGroup.MapGet("/requests", () => Results.Ok(new { Count = 378 }));

app.Run();