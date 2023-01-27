using RateLimiterSamples.RateLimitedClient;
using System.Threading;
using System;
using System.Threading.RateLimiting;

var options = new FixedWindowRateLimiterOptions
{
    PermitLimit = 2,
    Window = TimeSpan.FromSeconds(5),
    AutoReplenishment = true
};

// Create an HTTP client with the client-side rate limited handler.
using HttpClient client = new(handler: new ClientSideRateLimitedHandler(rateLimiter: new FixedWindowRateLimiter(options)));

// Create 100 urls with a unique query string.
for (int i = 0; i < 100; i++)
{
    GetAsync(client, $"http://localhost:5120/api/hello").Wait();
    Thread.Sleep(500);
}


Console.ReadLine();

static async Task GetAsync(HttpClient client, string url)
{
    using var response = await client.GetAsync(url);

    Console.WriteLine($"URL: {url}, HTTP status code: {response.StatusCode} ({(int)response.StatusCode})");
}
