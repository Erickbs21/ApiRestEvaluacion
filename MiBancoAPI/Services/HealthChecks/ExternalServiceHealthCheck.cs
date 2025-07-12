using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MiBancoAPI.Services;

public class ExternalServiceHealthCheck : IHealthCheck
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<ExternalServiceHealthCheck> _logger;

    public ExternalServiceHealthCheck(IHttpClientFactory httpClientFactory, ILogger<ExternalServiceHealthCheck> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            // Simular verificación de servicios externos
            // En un caso real, harías una llamada HTTP real
            await Task.Delay(100, cancellationToken); // Simular latencia

            var isHealthy = true; // Simular que el servicio está disponible

            if (isHealthy)
            {
                return HealthCheckResult.Healthy("Servicios externos simulados funcionando correctamente");
            }

            return HealthCheckResult.Degraded("Servicios externos con problemas menores");
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("Health check cancelado por timeout");
            return HealthCheckResult.Unhealthy("Timeout al verificar servicios externos");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al verificar servicios externos");
            return HealthCheckResult.Unhealthy("Servicios externos no disponibles", ex);
        }
    }
}
