SetWorkingDir, K:\CSharp\RateLimiterSamples\1_RateLimiterSamples.BuildInLimiters

sbombardatoreScriptFreeUser := ".\Scripts\2_2-fixedwindow-limiter-global-limiter-health.ps1"
sbombardatoreScriptPaidUser := ".\Scripts\2_2-fixedwindow-limiter-global-limiter-hello.ps1"

Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "dotnet run"
sleep, 4000
Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "%sbombardatoreScriptFreeUser%"
Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "%sbombardatoreScriptPaidUser%"