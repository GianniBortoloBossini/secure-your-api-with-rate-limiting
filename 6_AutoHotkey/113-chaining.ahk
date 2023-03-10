SetWorkingDir, K:\CSharp\RateLimiterSamples\1_RateLimiterSamples.BuildInLimiters

sbombardatoreScript := ".\Scripts\5_5-rate-limit-chaining.ps1"

Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "dotnet run"
sleep, 4000
Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "%sbombardatoreScript%"