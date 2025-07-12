using System.ComponentModel.DataAnnotations;

namespace MiBancoAPI.Models.DTOs;

public record PagoCreateDto(
    [Required]
    [StringLength(13, MinimumLength = 13)]
    [RegularExpression(@"^\d{13}$", ErrorMessage = "El DPI debe contener solo números")]
    string DPICliente,

    [Required]
    [Range(0.01, 100000, ErrorMessage = "El monto debe estar entre Q0.01 y Q100,000")]
    decimal Monto,

    [Required]
    [StringLength(200, MinimumLength = 5)]
    string Concepto,

    [Required]
    [AllowedValues("Transferencia", "Deposito", "Retiro", "Pago")]
    string TipoPago,

    string NumeroReferencia = ""
);
