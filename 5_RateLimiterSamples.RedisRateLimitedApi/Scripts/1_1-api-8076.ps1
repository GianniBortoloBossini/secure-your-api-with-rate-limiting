$Command = "K:\CSharp\RateLimiterSamples\0_Sbombardatore\bin\Debug\net7.0\RateLimiterSamples.Sbombardatore.exe"
$Parms = "port=8076 resource=api/hello user-identity=free delay=500 iterations=100 concurrency=1 project=redis"

$Parms = $Parms.Split(" ")
while($val -ne 50)
{
    $val++
    & "$Command" $Parms
    Start-Sleep -Seconds 1
}