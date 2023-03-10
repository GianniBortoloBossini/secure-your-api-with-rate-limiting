SetWorkingDir, K:\CSharp\RateLimiterSamples\4_RateLimiterSamples.AspNetCoreRateLimit

sbombardatoreScript := ".\Scripts\1_1-polling.ps1"

Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "dotnet run"
sleep, 4000
Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "%sbombardatoreScript%"