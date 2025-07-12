using FluentValidation;
using MiBancoAPI.Models.DTOs.Request;

namespace MiBancoAPI.Validators;

public class ClienteCreateDtoValidator : AbstractValidator<ClienteCreateDto>
{
    public ClienteCreateDtoValidator()
    {
        RuleFor(x => x.DPI)
            .NotEmpty().WithMessage("El DPI es requerido")
            .Length(13).WithMessage("El DPI debe tener exactamente 13 dígitos")
            .Matches(@"^\d{13}$").WithMessage("El DPI debe contener solo números");

        RuleFor(x => x.Nombres)
            .NotEmpty().WithMessage("Los nombres son requeridos")
            .Length(2, 100).WithMessage("Los nombres deben tener entre 2 y 100 caracteres")
            .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$").WithMessage("Los nombres solo pueden contener letras y espacios");

        RuleFor(x => x.Apellidos)
            .NotEmpty().WithMessage("Los apellidos son requeridos")
            .Length(2, 100).WithMessage("Los apellidos deben tener entre 2 y 100 caracteres")
            .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$").WithMessage("Los apellidos solo pueden contener letras y espacios");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es requerido")
            .EmailAddress().WithMessage("El formato del email no es válido");

        RuleFor(x => x.Telefono)
            .NotEmpty().WithMessage("El teléfono es requerido")
            .Matches(@"^\d{8}$").WithMessage("El teléfono debe tener 8 dígitos");

        RuleFor(x => x.FechaNacimiento)
            .NotEmpty().WithMessage("La fecha de nacimiento es requerida")
            .Must(BeValidAge).WithMessage("El cliente debe ser mayor de 18 años y menor de 100 años");

        RuleFor(x => x.Direccion)
            .NotEmpty().WithMessage("La dirección es requerida")
            .Length(10, 200).WithMessage("La dirección debe tener entre 10 y 200 caracteres");

        RuleFor(x => x.SaldoInicial)
            .GreaterThanOrEqualTo(0).WithMessage("El saldo inicial no puede ser negativo")
            .LessThanOrEqualTo(1000000).WithMessage("El saldo inicial no puede exceder Q1,000,000");
    }

    private static bool BeValidAge(DateTime fechaNacimiento)
    {
        var edad = DateTime.UtcNow.Year - fechaNacimiento.Year;
        if (DateTime.UtcNow.DayOfYear < fechaNacimiento.DayOfYear)
            edad--;

        return edad >= 18 && edad <= 100;
    }
}
