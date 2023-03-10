SetWorkingDir, K:\CSharp\RateLimiterSamples\2_RateLimiterSamples.BuildInLimiters.Mvc

sbombardatoreScriptHello := ".\Scripts\1_1-hello-resource-calls.ps1"
sbombardatoreScriptHealth := ".\Scripts\1_2-health-resource-calls.ps1"
sbombardatoreScriptCiao := ".\Scripts\1_3-ciao-resource-calls.ps1"

Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "dotnet run"
sleep, 4000
Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "%sbombardatoreScriptHello%"
Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "%sbombardatoreScriptHealth%"
Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "%sbombardatoreScriptCiao%"