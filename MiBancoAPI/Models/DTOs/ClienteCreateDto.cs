using System.ComponentModel.DataAnnotations;

namespace MiBancoAPI.Models.DTOs;

public record ClienteCreateDto(
    [Required]
    [StringLength(13, MinimumLength = 13, ErrorMessage = "El DPI debe tener exactamente 13 dígitos")]
    [RegularExpression(@"^\d{13}$", ErrorMessage = "El DPI debe contener solo números")]
    string DPI,

    [Required]
    [StringLength(100, MinimumLength = 2)]
    string Nombres,

    [Required]
    [StringLength(100, MinimumLength = 2)]
    string Apellidos,

    [Required]
    [EmailAddress]
    string Email,

    [Required]
    [Phone]
    string Telefono,

    [Required]
    DateTime FechaNacimiento,

    [Required]
    [StringLength(200, MinimumLength = 10)]
    string Direccion,

    [Range(0, 1000000)]
    decimal SaldoInicial = 0
);
