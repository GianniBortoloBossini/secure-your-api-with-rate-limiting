# Creazione token
Utente paid: 
dotnet user-jwts create --scope "paid" --role "user" --name "user_paid"
Utente free:
dotnet user-jwts create --scope "free" --role "user" --name "user_free"
Utente admin:
dotnet user-jwts create --scope "admin" --role "user" --name "user_admin"


# RateLimiterSamples.BuildInLimiters

## ConcurrencyLimiter
.\RateLimiterSamples.Sbombardatore.exe port=5120 resource=api/hello user-identity=free delay=500 iterations=10 concurrency=1
.\RateLimiterSamples.Sbombardatore.exe port=5120 resource=api/hello user-identity=free delay=500 iterations=10 concurrency=2
.\RateLimiterSamples.Sbombardatore.exe port=5120 resource=api/hello user-identity=free delay=500 iterations=10 concurrency=3
.\RateLimiterSamples.Sbombardatore.exe port=5120 resource=api/hello user-identity=free delay=500 iterations=10 concurrency=7
.\RateLimiterSamples.Sbombardatore.exe port=5120 resource=api/hello user-identity=free delay=500 iterations=10 concurrency=10

## FixedWindowLimiter

### 4 - FixedWindowLimiter 
Con questo comando tutte le richieste vengono servite
.\RateLimiterSamples.Sbombardatore.exe port=5120 resource=api/hello delay=1000 concurrency=2 iterations=10

Con questo comando una viene respinta
.\RateLimiterSamples.Sbombardatore.exe port=5120 resource=api/hello delay=1000 concurrency=3 iterations=10

### 5 - FixedWindowLimiter con coda
Per testarlo, 2 finestre con il seguente comando
.\RateLimiterSamples.Sbombardatore.exe port=5120 resource=api/hello delay=500 concurrency=1 iterations=10

La prima esecuzione servirà le prime due chiamate e poi resterà bloccata fino al termine dello scodamento che avviene alla fine della finestra (1 minuto), l'altra darà sempre 429.

### 6 - FixedWindowLimiter con retry-after
Per testarlo, 1 finestra con il seguente comando
.\RateLimiterSamples.Sbombardatore.exe port=5120 resource=api/hello delay=1000 concurrency=1 iterations=10
Verrà valorizzato l'header Retry-After

## SlidingWindowLimiter

### 7 - SlidingWindowLimiter 
Per testarlo, 1 finestra con il seguente comando
.\RateLimiterSamples.Sbombardatore.exe port=5120 resource=api/hello delay=500 concurrency=1 iterations=100 user-identity=free jitter=2000

## TokenBucketLimiter

### 9 - TokenBucketLimiter
Per testarlo, 1 finestra con il seguente comando
.\RateLimiterSamples.Sbombardatore.exe port=5120 resource=api/hello delay=500 concurrency=1 iterations=100 user-identity=free

## Policy, gruppi, abilitazione/disabilitazione selettiva e chaining

### 8 - Disabilitazione risorsa /health
Demo:
- Lanciare url
	.\RateLimiterSamples.Sbombardatore.exe port=5120 resource=api/health delay=1000 concurrency=1 iterations=100 user-identity=free
- Si vedono 429 
- Nelle api decommentare //.DisableRateLimiting(); alla risorsa /health

### 9 - Abilitazione selettiva per endpoint e gruppi (con custom policy)

Sulla risorsa buy, gli account l'account free ha 1 chiamata, mentre quello paid ne ha 10
.\RateLimiterSamples.Sbombardatore.exe port=5120 resource=api/buy delay=500 concurrency=1 iterations=100 user-identity=free
.\RateLimiterSamples.Sbombardatore.exe port=5120 resource=api/buy delay=500 concurrency=1 iterations=100 user-identity=paid

Sulla risorsa statistics/*, gli account l'account free ha 1 chiamata, mentre quello paid ne ha 10
.\RateLimiterSamples.Sbombardatore.exe port=5120 resource=api/statistics/users delay=1000 concurrency=1 iterations=100 user-identity=free
.\RateLimiterSamples.Sbombardatore.exe port=5120 resource=api/statistics/requests delay=500 concurrency=1 iterations=100 user-identity=paid

### 10 - Chaining
- 10 OK nei primi 10 secondi (prima policy)
- 1 nei secondi 5 (seconda policy)
- poi si riparte
.\RateLimiterSamples.Sbombardatore.exe port=5120 resource=api/hello delay=500 concurrency=1 iterations=100 user-identity=free