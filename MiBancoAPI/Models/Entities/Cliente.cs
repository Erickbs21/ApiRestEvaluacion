using System.ComponentModel.DataAnnotations;

namespace MiBancoAPI.Models.Entities;

public class Cliente : BaseEntity
{
    [Required]
    [StringLength(13, MinimumLength = 13)]
    public required string DPI { get; set; }

    [Required]
    [StringLength(100)]
    public required string Nombres { get; set; }

    [Required]
    [StringLength(100)]
    public required string Apellidos { get; set; }

    [EmailAddress]
    public required string Email { get; set; }

    [Phone]
    public required string Telefono { get; set; }

    public DateTime FechaNacimiento { get; set; }

    [StringLength(200)]
    public required string Direccion { get; set; }

    [Range(0, double.MaxValue)]
    public decimal SaldoCuenta { get; set; }

    public EstadoCliente Estado { get; set; } = EstadoCliente.Activo;

    public DateTime UltimaActividad { get; set; } = DateTime.UtcNow;

    // Propiedades calculadas
    public string NombreCompleto => $"{Nombres} {Apellidos}";

    public int Edad => DateTime.UtcNow.Year - FechaNacimiento.Year -
                      (DateTime.UtcNow.DayOfYear < FechaNacimiento.DayOfYear ? 1 : 0);

    public string SaldoFormateado => $"Q{SaldoCuenta:N2}";

    public bool Activo => Estado == EstadoCliente.Activo;

    // Navegación
    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}

public enum EstadoCliente
{
    Activo = 1,
    Inactivo = 2,
    Suspendido = 3,
    Bloqueado = 4
}
