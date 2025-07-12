using System.ComponentModel.DataAnnotations;

namespace MiBancoAPI.Models.Entities;

public class Pago : BaseEntity
{
    [Required]
    [StringLength(13, MinimumLength = 13)]
    public required string DPICliente { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Monto { get; set; }

    [Required]
    [StringLength(200)]
    public required string Concepto { get; set; }

    [Required]
    public TipoPago TipoPago { get; set; }

    public required string NumeroReferencia { get; set; }

    public DateTime FechaPago { get; set; } = DateTime.UtcNow;

    public EstadoPago Estado { get; set; } = EstadoPago.Pendiente;

    public string? NotasAdicionales { get; set; }

    public decimal? ComisionAplicada { get; set; }

    // Propiedades calculadas
    public string MontoFormateado => $"Q{Monto:N2}";

    public decimal MontoTotal => Monto + (ComisionAplicada ?? 0);

    // Navegación
    public virtual Cliente? Cliente { get; set; }
}

public enum TipoPago
{
    Transferencia = 1,
    Deposito = 2,
    Retiro = 3,
    PagoServicios = 4,
    PagoPrestamo = 5
}

public enum EstadoPago
{
    Pendiente = 1,
    Procesando = 2,
    Completado = 3,
    Fallido = 4,
    Cancelado = 5,
    Revertido = 6
}
