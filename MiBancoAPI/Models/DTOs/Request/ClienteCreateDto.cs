namespace MiBancoAPI.Models.DTOs.Request;

public record ClienteCreateDto(
    string DPI,
    string Nombres,
    string Apellidos,
    string Email,
    string Telefono,
    DateTime FechaNacimiento,
    string Direccion,
    decimal SaldoInicial = 0
);
