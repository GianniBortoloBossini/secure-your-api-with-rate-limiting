﻿SetWorkingDir, K:\CSharp\RateLimiterSamples\1_RateLimiterSamples.BuildInLimiters

sbombardatoreScript := ".\Scripts\2_1-fixedwindow-limiter.ps1"

Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "dotnet run"
sleep, 4000
Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "%sbombardatoreScript%"