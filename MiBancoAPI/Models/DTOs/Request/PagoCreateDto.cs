using MiBancoAPI.Models.Entities;

namespace MiBancoAPI.Models.DTOs.Request;

public record PagoCreateDto(
    string DPICliente,
    decimal Monto,
    string Concepto,
    TipoPago TipoPago,
    string? NumeroReferencia = null,
    string? NotasAdicionales = null
);
