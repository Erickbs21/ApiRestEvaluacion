using System;

namespace MiBancoAPI.Models.Entities;

public enum LogNivel
{
    Info,
    Warning,
    Error,
    Debug
}

public class LogEntry : BaseEntity
{
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public required string Accion { get; set; }
    public required string Detalles { get; set; }
    public string Usuario { get; set; } = "Sistema";
    public required string Endpoint { get; set; }
    public LogNivel Nivel { get; set; } = LogNivel.Info;
}