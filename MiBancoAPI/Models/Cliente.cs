using System.ComponentModel.DataAnnotations;

namespace MiBancoAPI.Models;

public class Cliente
{
    public int Id { get; set; }

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

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    public bool Activo { get; set; } = true;

    // Propiedad calculada
    public string NombreCompleto => $"{Nombres} {Apellidos}";

    // Propiedad calculada para edad
    public int Edad => DateTime.Now.Year - FechaNacimiento.Year -
                      (DateTime.Now.DayOfYear < FechaNacimiento.DayOfYear ? 1 : 0);
}
