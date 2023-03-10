SetWorkingDir, K:\CSharp\RateLimiterSamples\1_RateLimiterSamples.BuildInLimiters

sbombardatoreScript := ".\Scripts\1_4-concurrency-limiter-3-concurrent-with-429.ps1"

Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "dotnet run"
Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "%sbombardatoreScript%"