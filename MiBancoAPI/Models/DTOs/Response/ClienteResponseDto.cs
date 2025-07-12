using MiBancoAPI.Models.Entities;

namespace MiBancoAPI.Models.DTOs.Response;

public record ClienteResponseDto(
    int Id,
    string DPI,
    string NombreCompleto,
    string Email,
    string Telefono,
    int Edad,
    string SaldoFormateado,
    EstadoCliente Estado,
    DateTime FechaCreacion,
    DateTime UltimaActividad
);
