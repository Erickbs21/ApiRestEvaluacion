using MiBancoAPI.Models.Entities;

namespace MiBancoAPI.Models.DTOs.Response;

public record PagoResponseDto(
    int Id,
    string DPICliente,
    string MontoFormateado,
    string Concepto,
    TipoPago TipoPago,
    string NumeroReferencia,
    EstadoPago Estado,
    DateTime FechaPago,
    string? NotasAdicionales = null
);
