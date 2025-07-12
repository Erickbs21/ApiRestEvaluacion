using MiBancoAPI.Models.Entities;

namespace MiBancoAPI.Services;

public interface ILogService
{
    Task RegistrarLogAsync(string accion, string detalles, string endpoint, LogNivel nivel = LogNivel.Info);
    Task<List<LogEntry>> ObtenerLogsAsync();
    Task<List<LogEntry>> ObtenerLogsPorFechaAsync(DateTime fecha);
    Task<List<LogEntry>> ObtenerLogsPorNivelAsync(LogNivel nivel);
}
