using FluentValidation.Results;
using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;
using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Domain.Repositories.Customers;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Application.UseCases.Customer.CreateCustomer
{
    public class CreateCustomerUseCase : ICreateCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerUseCase(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateCustomerResponseDto> Execute(CreateCustomerRequestDto request)
        {
            await Validate(request);

            var customer = new Domain.Entities.Customer
            {
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Addresses = new List<Address>(),
                CustomerType = (CustomerType)request.CustomerType,
                Document = request.Document,
            };

            await _customerRepository.Add(customer);

            await _unitOfWork.Commit();

            return new CreateCustomerResponseDto
            {
                Addresses = new List<AddressRequestDto>(),
                CustomerType = request.CustomerType,
                Document = request.Document,
                Email = request.Email,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
            };
        }

        private async Task Validate(CreateCustomerRequestDto request)
        {
            var result = new CreateCustomerValidator().Validate(request);

            var emailExist = await _customerRepository.ExistActiveCustomerWithEmail(request.Email);
            if (emailExist)
            {
                result.Errors.Add(new ValidationFailure(string.Empty, "User with this e-mail already registered"));
            }

            if (result.IsValid.Equals(false))
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
