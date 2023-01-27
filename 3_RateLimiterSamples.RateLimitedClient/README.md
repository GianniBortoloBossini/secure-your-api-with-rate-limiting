1. All'interno del progetto `1_RateLimiterSamples.BuildInLimiters` decommentare l'esempio/region `4 - FixedWindowLimiter`
2. Avviare le api con il comando `dotnet run` da powershell
3. Lanciare l'eseguibile del client auto-limitato del progetto `RateLimiterSamples.RateLimitedClient`
4. Verificare che nella shell delle api appaiano le loggate `Request received!` quando il client riceve risposta `200 OK`