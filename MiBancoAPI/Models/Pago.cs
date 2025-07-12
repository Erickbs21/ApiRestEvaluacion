using System.ComponentModel.DataAnnotations;

namespace MiBancoAPI.Models;

public class Pago
{
    public int Id { get; set; }

    [Required]
    [StringLength(13, MinimumLength = 13)]
    public required string DPICliente { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a cero")]
    public decimal Monto { get; set; }

    [Required]
    [StringLength(200)]
    public required string Concepto { get; set; }

    [Required]
    public required string TipoPago { get; set; } // Transferencia, Deposito, Retiro

    public required string NumeroReferencia { get; set; }

    public DateTime FechaPago { get; set; } = DateTime.Now;

    public EstadoPago Estado { get; set; } = EstadoPago.Completado;

    // Propiedad calculada para mostrar el monto formateado
    public string MontoFormateado => $"Q{Monto:N2}";
}

public enum EstadoPago
{
    Pendiente,
    Completado,
    Fallido,
    Cancelado
}
