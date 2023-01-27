using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Globalization;
using Spectre.Console;
using System;

const string BUILTIN_TOKEN_PAID_USER = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InVzZXJfcGFpZCIsInN1YiI6InVzZXJfcGFpZCIsImp0aSI6IjVhZjRhMmFkIiwic2NvcGUiOiJwYWlkIiwicm9sZSI6InVzZXIiLCJhdWQiOlsiaHR0cDovL2xvY2FsaG9zdDozNjkwIiwiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzMTEiLCJodHRwOi8vbG9jYWxob3N0OjUxMjAiLCJodHRwczovL2xvY2FsaG9zdDo3MjA2Il0sIm5iZiI6MTY3NDY4NzI5MCwiZXhwIjoxNjgyNDYzMjkwLCJpYXQiOjE2NzQ2ODcyOTEsImlzcyI6ImRvdG5ldC11c2VyLWp3dHMifQ.NFj0HYpcl4WtGxIg3LWZHudOzKqeagFt3-LGwuSOAxE";
const string BUILTIN_TOKEN_FREE_USER = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InVzZXJfZnJlZSIsInN1YiI6InVzZXJfZnJlZSIsImp0aSI6IjllZGMwZDNkIiwic2NvcGUiOiJmcmVlIiwicm9sZSI6InVzZXIiLCJhdWQiOlsiaHR0cDovL2xvY2FsaG9zdDozNjkwIiwiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzMTEiLCJodHRwOi8vbG9jYWxob3N0OjUxMjAiLCJodHRwczovL2xvY2FsaG9zdDo3MjA2Il0sIm5iZiI6MTY3NDY2ODkwMywiZXhwIjoxNjgyNDQ0OTAzLCJpYXQiOjE2NzQ2Njg5MDQsImlzcyI6ImRvdG5ldC11c2VyLWp3dHMifQ.4hKYCqLRCrIV2Kx5lbxFqpWm08rMq24Y3EpXuDpRUBs";
const string BUILTIN_TOKEN_ADMIN_USER = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InVzZXJfYWRtaW4iLCJzdWIiOiJ1c2VyX2FkbWluIiwianRpIjoiZTk2MGI5MmIiLCJzY29wZSI6ImFkbWluIiwicm9sZSI6InVzZXIiLCJhdWQiOlsiaHR0cDovL2xvY2FsaG9zdDozNjkwIiwiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzMTEiLCJodHRwOi8vbG9jYWxob3N0OjUxMjAiLCJodHRwczovL2xvY2FsaG9zdDo3MjA2Il0sIm5iZiI6MTY3NDY2ODkzNCwiZXhwIjoxNjgyNDQ0OTM0LCJpYXQiOjE2NzQ2Njg5MzUsImlzcyI6ImRvdG5ldC11c2VyLWp3dHMifQ.9fdmk4s6WJZCv5Fl2Dh_NzVGO4vV3ohyj_iFxEpdaEw";

const string REDIS_TOKEN_PAID_USER = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InVzZXJfcGFpZCIsInN1YiI6InVzZXJfcGFpZCIsImp0aSI6IjdlOGY5NiIsInNjb3BlIjoicGFpZCIsInJvbGUiOiJ1c2VyIiwiYXVkIjpbImh0dHA6Ly9sb2NhbGhvc3Q6NDI3ODkiLCJodHRwczovL2xvY2FsaG9zdDowIiwiaHR0cDovL2xvY2FsaG9zdDo1MjMxIl0sIm5iZiI6MTY3NDYwMTE1OSwiZXhwIjoxNjgyMzc3MTU5LCJpYXQiOjE2NzQ2MDExNjEsImlzcyI6ImRvdG5ldC11c2VyLWp3dHMifQ.uy3AtFbLyyvtl7SKLgdvpeEqC0n1KkqOh9Cx2HDs4bM";
const string REDIS_TOKEN_FREE_USER = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InVzZXJfZnJlZSIsInN1YiI6InVzZXJfZnJlZSIsImp0aSI6IjYwNzQxZmUiLCJzY29wZSI6ImZyZWUiLCJyb2xlIjoidXNlciIsImF1ZCI6WyJodHRwOi8vbG9jYWxob3N0OjQyNzg5IiwiaHR0cHM6Ly9sb2NhbGhvc3Q6MCIsImh0dHA6Ly9sb2NhbGhvc3Q6NTIzMSJdLCJuYmYiOjE2NzQ2MDA5MDAsImV4cCI6MTY4MjM3NjkwMCwiaWF0IjoxNjc0NjAwOTAxLCJpc3MiOiJkb3RuZXQtdXNlci1qd3RzIn0.uT-32N2wYQZbjNxfCA8AjVEhpC2-SKSRyyTHn0zDTjM";

var port = 5120;
var delay = 1000;
var iterations = 200;
var concurrency = 1;
var resource = "api/hello";
var userIdentity = "";
var jitter = 0;
var project = "builtin";
var window = 0;

foreach (var arg in args)
{
    if (arg.StartsWith("port"))
        port = int.Parse(arg.Split('=')[1]);
    if (arg.StartsWith("resource"))
        resource = arg.Split('=')[1];
    if (arg.StartsWith("delay"))
        delay = int.Parse(arg.Split('=')[1]);
    if (arg.StartsWith("user-identity"))
        userIdentity = arg.Split('=')[1];
    if (arg.StartsWith("concurrency"))
        concurrency = int.Parse(arg.Split('=')[1]);
    if (arg.StartsWith("iterations"))
        iterations = int.Parse(arg.Split('=')[1]);
    if (arg.StartsWith("jitter"))
        jitter = int.Parse(arg.Split('=')[1]);
    if (arg.StartsWith("project"))
        project = arg.Split('=')[1];
    if (arg.StartsWith("window"))
        window = int.Parse(arg.Split('=')[1]);
}

Uri address = new Uri(String.Format("http://localhost:{0}/{1}", port, resource));

var table = new Table();
table.AddColumn("TS Sent");
table.AddColumn("TS Received");
table.AddColumn("StatusCode");
table.AddColumn("User");
table.AddColumn("Retry-After");
table.AddColumn("Index");

Random rand = new Random();

AnsiConsole.Live(table)
    .AutoClear(false)
    .Overflow(VerticalOverflow.Ellipsis)
    .Cropping(VerticalOverflowCropping.Top)
    .Start(ctx =>
    {
        void LogResult(LiveDisplayContext ctx, Table table, object index, string token, Uri address)
        {
            var begin = DateTime.Now;

            try 
            {
                HttpClient client = new() { BaseAddress = address };
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                begin = DateTime.Now;
                
                var response = client.GetAsync("").Result;
                var retryAfterValue = response.Headers.TryGetValues("Retry-After", out IEnumerable<string> retryAfterList)
                    ? retryAfterList.First()
                    : " ";
                var userIdentifierValue = response.Headers.TryGetValues("x-user-identifier", out IEnumerable<string> userIdentifierList)
                    ? userIdentifierList.First()
                    : " ";
                table.AddRow(
                    begin.ToString("hh:mm:ss.fff", CultureInfo.InvariantCulture),
                    DateTime.Now.ToString("hh:mm:ss.fff", CultureInfo.InvariantCulture),
                    $"[{(response.IsSuccessStatusCode ? "green" : "red")}]{(int)response.StatusCode}[/]",
                    userIdentifierValue,
                    retryAfterValue,
                    index.ToString()
                );
            }
            catch
            {
                table.AddRow(
                    begin.ToString("hh:mm:ss.fff", CultureInfo.InvariantCulture),
                    DateTime.Now.ToString("hh:mm:ss.fff", CultureInfo.InvariantCulture),
                    "???",
                    "???",
                    "",
                    index.ToString()
                );
            }
            ctx.Refresh();
        }

        if (window > 0)
        {
            async Task Run(Action action, TimeSpan period, CancellationToken cancellationToken)
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(period, cancellationToken);

                    if (!cancellationToken.IsCancellationRequested)
                        action();
                }
            }
            Run(() => {
                table.AddRow(
                    "- - -",
                    "- - -",
                    "- - -",
                    "- - -",
                    "- - -",
                    "- - -"
                );
            }, TimeSpan.FromSeconds(window), CancellationToken.None).ConfigureAwait(false);
        }

        for (int t = 0; t < iterations; t++)
        {
            List<Task> tasks = new List<Task>();
            for (int i = 0; i < concurrency; i++)
            {
                if (string.IsNullOrWhiteSpace(userIdentity) || userIdentity == "free")
                    tasks.Add(new TaskFactory().StartNew((index) => LogResult(ctx, table, index, project == "builtin" ? BUILTIN_TOKEN_FREE_USER : REDIS_TOKEN_FREE_USER, address), i));
                if (string.IsNullOrWhiteSpace(userIdentity) || userIdentity == "paid")
                    tasks.Add(new TaskFactory().StartNew((index) => LogResult(ctx, table, index, project == "builtin" ? BUILTIN_TOKEN_PAID_USER : REDIS_TOKEN_PAID_USER, address), i));
                if (userIdentity == "admin")
                    tasks.Add(new TaskFactory().StartNew((index) => LogResult(ctx, table, index, BUILTIN_TOKEN_ADMIN_USER, address), i));
            }
            Task.WaitAll(tasks.ToArray());
            Thread.Sleep(delay + rand.Next(0, jitter));
        }
    });