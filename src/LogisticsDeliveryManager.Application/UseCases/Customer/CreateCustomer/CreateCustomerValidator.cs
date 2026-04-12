using FluentValidation;
using LogisticsDeliveryManager.Communication.Requests;

namespace LogisticsDeliveryManager.Application.UseCases.Customer.CreateCustomer
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerRequestJson>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.");
            RuleFor(x => x.Document)
                .NotEmpty()
                .WithMessage("Document is required.");
            RuleFor(x => x.CustomerType).IsInEnum().WithMessage("CustomerType is required");
            RuleFor(x => x.Addresses).NotEmpty().WithMessage("Addresses is required");
            RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("E-mail is required");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required");
        }
    }
}
