namespace MiBancoAPI.Models;

public class LogEntry
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public required string Accion { get; set; }
    public required string Detalles { get; set; }
    public string Usuario { get; set; } = "Sistema";
    public required string Endpoint { get; set; }
    public LogLevel Nivel { get; set; } = LogLevel.Info;
}

public enum LogLevel
{
    Info,
    Warning,
    Error,
    Debug
}
