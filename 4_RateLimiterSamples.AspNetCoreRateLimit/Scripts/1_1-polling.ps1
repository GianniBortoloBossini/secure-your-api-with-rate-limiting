while($val -ne 50)
{
    $val++
    Invoke-WebRequest -Uri "http://localhost:5146/"
    Start-Sleep -Seconds 1
}