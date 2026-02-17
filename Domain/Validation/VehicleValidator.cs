using API_Veiculos.Domain.Entity;
using FluentValidation;

namespace API_Veiculos.Domain.Validation
{
    public class VehicleValidator : AbstractValidator<Vehicle>
    {
        public VehicleValidator()
        {
            RuleFor(vehicle => vehicle.Id)
                .NotEmpty()
                .NotNull();
            RuleFor(vehicle => vehicle.DescVeiculo)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(vehicle => vehicle.MarcaVeiculo)
                .IsInEnum()
                .NotNull();
            RuleFor(vehicle => vehicle.ModeloVeiculo)
                .NotNull()
                .NotEmpty()
                .MaximumLength(30);               
        }
    }
}
