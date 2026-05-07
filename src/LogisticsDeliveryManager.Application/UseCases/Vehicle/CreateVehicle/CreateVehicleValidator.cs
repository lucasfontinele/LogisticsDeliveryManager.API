using FluentValidation;
using LogisticsDeliveryManager.Communication.Requests;

namespace LogisticsDeliveryManager.Application.UseCases.Vehicle.CreateVehicle
{
    public class CreateVehicleValidator : AbstractValidator<CreateVehicleRequestJson>
    {
        public CreateVehicleValidator()
        {
            RuleFor(x => x.LicensePlate)
                .NotEmpty()
                .WithMessage("License plate is required.");
            RuleFor(x => x.Model)
                .NotEmpty()
                .WithMessage("Model is required.");
            RuleFor(x => x.WeightCapacity)
                .GreaterThan(0)
                .WithMessage("Weight capacity must be a positive value.");
            RuleFor(x => x.VolumeCapacity)
                .GreaterThan(0)
                .WithMessage("Volume capacity must be a positive value.");
            RuleFor(x => x.CompartmentType).IsInEnum().WithMessage("Compartment type is required");
        }
    }
}
