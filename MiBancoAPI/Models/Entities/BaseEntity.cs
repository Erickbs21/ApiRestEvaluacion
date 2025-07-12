namespace MiBancoAPI.Models.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    public DateTime? FechaModificacion { get; set; }
    public string CreadoPor { get; set; } = "Sistema";
    public string? ModificadoPor { get; set; }
    public bool EstaActivo { get; set; } = true;
}
