using FluentValidation;
using MiBancoAPI.Models.DTOs.Request;
using MiBancoAPI.Models.Entities;

namespace MiBancoAPI.Validators;

public class PagoCreateDtoValidator : AbstractValidator<PagoCreateDto>
{
    public PagoCreateDtoValidator()
    {
        RuleFor(x => x.DPICliente)
            .NotEmpty().WithMessage("El DPI del cliente es requerido")
            .Length(13).WithMessage("El DPI debe tener exactamente 13 dígitos")
            .Matches(@"^\d{13}$").WithMessage("El DPI debe contener solo números");

        RuleFor(x => x.Monto)
            .GreaterThan(0).WithMessage("El monto debe ser mayor a cero")
            .LessThanOrEqualTo(100000).WithMessage("El monto no puede exceder Q100,000 por transacción");

        RuleFor(x => x.Concepto)
            .NotEmpty().WithMessage("El concepto es requerido")
            .Length(5, 200).WithMessage("El concepto debe tener entre 5 y 200 caracteres");

        RuleFor(x => x.TipoPago)
            .IsInEnum().WithMessage("El tipo de pago no es válido");

        RuleFor(x => x.NumeroReferencia)
            .MaximumLength(50).WithMessage("El número de referencia no puede exceder 50 caracteres")
            .When(x => !string.IsNullOrEmpty(x.NumeroReferencia));

        RuleFor(x => x.NotasAdicionales)
            .MaximumLength(500).WithMessage("Las notas adicionales no pueden exceder 500 caracteres")
            .When(x => !string.IsNullOrEmpty(x.NotasAdicionales));
    }
}
