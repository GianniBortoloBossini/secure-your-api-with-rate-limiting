using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

var rateLimiterConfig = new RateLimiterOptions();
rateLimiterConfig.AddFixedWindowLimiter("ApiLimit", options =>
{
    options.PermitLimit = 10;
    options.Window = TimeSpan.FromSeconds(10);
});
rateLimiterConfig.AddFixedWindowLimiter("HelloLimit", options =>
{
    options.PermitLimit = 5;
    options.Window = TimeSpan.FromSeconds(10);
});

// FUNZIONA TUTTO
//app.UseRateLimiter(rateLimiterConfig);

// CON app.UseRouting(); ATTENZIONE ALL'ORDINE!!!
// https://nicolaiarocci.com/on-implementing-the-asp.net-core-7-rate-limiting-middleware/

#region ORDINE NON CORRETTO: il rate limiting non si applica!
// app.UseRateLimiter(rateLimiterConfig);
// app.UseRouting();
#endregion

#region ORDINE CORRETTO!
app.UseRouting();
app.UseRateLimiter(rateLimiterConfig);
#endregion

app.MapControllers();

app.Run();
