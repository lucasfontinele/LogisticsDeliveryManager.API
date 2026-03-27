using FluentValidation.Results;
using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;

namespace LogisticsDeliveryManager.Application.UseCases.Customer.CreateCustomer
{
    public class CreateCustomerUseCase : ICreateCustomerUseCase
    {
        public async Task<CreateCustomerResponseDto> Execute(CreateCustomerRequestDto request)
        {
            Validate(request);

            return await Task.FromResult(new CreateCustomerResponseDto
            {
                Id = 1,
                Name = "John Doe",
                Document = "123456789",
                Address = new List<Communication.Requests.AddressRequestDto>
                {
                    new Communication.Requests.AddressRequestDto
                    {
                        Street = "123 Main St",
                        City = "Anytown",
                        State = "Anystate",
                        PostalCode = "12345"
                    }
                },
                CustomerType = Communication.Enums.CustomerTypeDto.Premium,
                PhoneNumber = "63984873881",
                Email = "contato@fontinele.dev"
            });
        }

        private void Validate(CreateCustomerRequestDto request)
        {
            var result = new CreateCustomerValidator().Validate(request);

            //var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);
            //if (emailExist)
            //{
            //    result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
            //}

            if (result.IsValid.Equals(false))
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                //throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
