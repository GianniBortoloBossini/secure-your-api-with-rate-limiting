SetWorkingDir, K:\CSharp\RateLimiterSamples\1_RateLimiterSamples.BuildInLimiters

sbombardatoreScriptFreeUser := ".\Scripts\5_2-rate-limit-for-free-when-buy-route-is-called.ps1"
sbombardatoreScriptPaidUser := ".\Scripts\5_3-rate-limit-for-paid-when-buy-route-is-called.ps1"
sbombardatoreScriptAdminUser := ".\Scripts\5_4-rate-limit-for-admin-when-buy-route-is-called.ps1"

Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "dotnet run"
sleep, 4000
Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "%sbombardatoreScriptFreeUser%"
Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "%sbombardatoreScriptPaidUser%"
Run, "C:\WINDOWS\System32\WindowsPowerShell\v1.0\powershell.exe" "-NoExit" "%sbombardatoreScriptAdminUser%"