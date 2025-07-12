using MiBancoAPI.Models.Entities;
using System.Collections.Concurrent;

namespace MiBancoAPI.Services;

public class LogService : ILogService
{
    private static readonly ConcurrentBag<LogEntry> _logs = new();

    public async Task RegistrarLogAsync(string accion, string detalles, string endpoint, LogNivel nivel = LogNivel.Info)
    {
        await Task.Delay(1); // Simular operación async

        var logEntry = new LogEntry
        {
            Id = _logs.Count + 1,
            Timestamp = DateTime.UtcNow,
            Accion = accion,
            Detalles = detalles,
            Usuario = "Sistema",
            Endpoint = endpoint,
            Nivel = nivel
        };

        _logs.Add(logEntry);
    }

    public async Task<List<LogEntry>> ObtenerLogsAsync()
    {
        await Task.Delay(10); // Simular operación async
        return _logs.OrderByDescending(l => l.Timestamp).ToList();
    }

    public async Task<List<LogEntry>> ObtenerLogsPorFechaAsync(DateTime fecha)
    {
        await Task.Delay(10); // Simular operación async
        return _logs.Where(l => l.Timestamp.Date == fecha.Date)
                   .OrderByDescending(l => l.Timestamp)
                   .ToList();
    }

    public async Task<List<LogEntry>> ObtenerLogsPorNivelAsync(LogNivel nivel)
    {
        await Task.Delay(10); // Simular operación async
        return _logs.Where(l => l.Nivel == nivel)
                   .OrderByDescending(l => l.Timestamp)
                   .ToList();
    }
}
