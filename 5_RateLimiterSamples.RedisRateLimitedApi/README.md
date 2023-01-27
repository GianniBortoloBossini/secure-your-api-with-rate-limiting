## Rate limit con contatori in Redis
1. Creare i bearer token assocati al progetto con i comandi
   dotnet user-jwts create --scope "free" --role "user" --name "user_free"

   eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InVzZXJfZnJlZSIsInN1YiI6InVzZXJfZnJlZSIsImp0aSI6IjYwNzQxZmUiLCJzY29wZSI6ImZyZWUiLCJyb2xlIjoidXNlciIsImF1ZCI6WyJodHRwOi8vbG9jYWxob3N0OjQyNzg5IiwiaHR0cHM6Ly9sb2NhbGhvc3Q6MCIsImh0dHA6Ly9sb2NhbGhvc3Q6NTIzMSJdLCJuYmYiOjE2NzQ2MDA5MDAsImV4cCI6MTY4MjM3NjkwMCwiaWF0IjoxNjc0NjAwOTAxLCJpc3MiOiJkb3RuZXQtdXNlci1qd3RzIn0.uT-32N2wYQZbjNxfCA8AjVEhpC2-SKSRyyTHn0zDTjM

   dotnet user-jwts create --scope "paid" --role "user" --name "user_paid"

   eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InVzZXJfcGFpZCIsInN1YiI6InVzZXJfcGFpZCIsImp0aSI6IjdlOGY5NiIsInNjb3BlIjoicGFpZCIsInJvbGUiOiJ1c2VyIiwiYXVkIjpbImh0dHA6Ly9sb2NhbGhvc3Q6NDI3ODkiLCJodHRwczovL2xvY2FsaG9zdDowIiwiaHR0cDovL2xvY2FsaG9zdDo1MjMxIl0sIm5iZiI6MTY3NDYwMTE1OSwiZXhwIjoxNjgyMzc3MTU5LCJpYXQiOjE2NzQ2MDExNjEsImlzcyI6ImRvdG5ldC11c2VyLWp3dHMifQ.uy3AtFbLyyvtl7SKLgdvpeEqC0n1KkqOh9Cx2HDs4bM
2. Scaricare container docker di Redis `docker run -d --name rate-limiting-redis -p 6379:6379 redis`
3. Avviare due istanze del progetto `5_RateLimiterSamples.RedisRateLimitedApi` con i comandi `dotnet run --urls http://localhost:8076` e `dotnet run --urls http://localhost:8077` da powershell
4. Lanciare da due prompt i seguenti comandi:
   .\RateLimiterSamples.Sbombardatore.exe port=8076 resource=api/hello user-identity=free delay=500 iterations=100 concurrency=1 project=redis
   .\RateLimiterSamples.Sbombardatore.exe port=8077 resource=api/hello user-identity=free delay=500 iterations=100 concurrency=1 project=redis