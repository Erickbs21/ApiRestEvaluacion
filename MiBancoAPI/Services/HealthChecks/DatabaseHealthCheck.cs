using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MiBancoAPI.Services;

public class DatabaseHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            // Simular verificación de base de datos
            // En un caso real, aquí verificarías la conexión a la BD
            var isHealthy = true;

            if (isHealthy)
            {
                return Task.FromResult(HealthCheckResult.Healthy("Base de datos simulada funcionando correctamente"));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("Base de datos no disponible"));
        }
        catch (Exception ex)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy("Error al verificar base de datos", ex));
        }
    }
}
