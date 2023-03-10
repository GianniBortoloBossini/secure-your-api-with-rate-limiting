SetWorkingDir, K:\CSharp\RateLimiterSamples\5_RateLimiterSamples.RedisRateLimitedApi

sbombardatoreScript8076 := ".\Scripts\1_1-api-8076.ps1"
sbombardatoreScript8077 := ".\Scripts\1_2-api-8077.ps1"

Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "dotnet run --urls http://localhost:8076"
sleep, 100
Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "dotnet run --urls http://localhost:8077"
sleep, 4000
Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "%sbombardatoreScript8076%"
Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "%sbombardatoreScript8077%"