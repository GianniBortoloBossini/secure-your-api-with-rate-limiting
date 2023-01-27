using RedisRateLimiting;
using RedisRateLimiting.AspNetCore;
using StackExchange.Redis;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

var redisOptions = ConfigurationOptions.Parse("localhost:6379,abortConnect=false");
var connectionMultiplexer = ConnectionMultiplexer.Connect(redisOptions);

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = 429;
    options.GlobalLimiter =
        PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            RedisRateLimitPartition.GetFixedWindowRateLimiter(
                partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                factory: partition => new RedisFixedWindowRateLimiterOptions
                {
                    ConnectionMultiplexerFactory = () => connectionMultiplexer,
                    PermitLimit = 6,
                    Window = TimeSpan.FromSeconds(20)
                }));
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

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

app.Run();