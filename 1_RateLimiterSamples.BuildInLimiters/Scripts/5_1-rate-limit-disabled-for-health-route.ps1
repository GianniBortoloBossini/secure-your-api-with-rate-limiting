$Command = "K:\CSharp\RateLimiterSamples\0_Sbombardatore\bin\Debug\net7.0\RateLimiterSamples.Sbombardatore.exe"
$Parms = "port=5120 resource=api/health delay=1000 concurrency=1 iterations=100 user-identity=free"

$Parms = $Parms.Split(" ")
& "$Command" $Parms