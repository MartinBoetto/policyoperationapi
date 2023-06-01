using FluentValidation;
using System.Linq;

namespace PolicyOperation.Core.Entidad.Policy
{
    public class PolicyRequestValidator : AbstractValidator<PolicyRequest>
    {
        public PolicyRequestValidator()
        {
            // FluentValidation Documentation: https://docs.fluentvalidation.net/en/latest/ 
            //EjemplO: 
            // RuleFor(p => p.Id)
            //     .GreaterThan(0)
            //         .WithMessage(p => $"El parámetro '{nameof(p.Id)}' debe ser mayor a 0.")
            //         .When(p => p.Id.HasValue);

            // RuleFor(p => p.Nombre)
            //     .Must((string nombre) => nombre.ToCharArray().All(c => char.IsLetter(c)))
            //         .WithMessage(p => $"El parámetro '{nameof(p.Nombre)}' debe contener sólo letras.")
            //         .When(p => !string.IsNullOrEmpty(p.Nombre));
        }
    }
}
