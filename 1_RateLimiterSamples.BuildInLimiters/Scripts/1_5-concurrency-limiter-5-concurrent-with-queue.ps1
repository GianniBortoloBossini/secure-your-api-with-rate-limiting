$Command = "K:\CSharp\RateLimiterSamples\0_Sbombardatore\bin\Debug\net7.0\RateLimiterSamples.Sbombardatore.exe"
$Parms = "port=5120 resource=api/hello user-identity=free delay=500 iterations=10 concurrency=10"

$Parms = $Parms.Split(" ")
& "$Command" $Parms