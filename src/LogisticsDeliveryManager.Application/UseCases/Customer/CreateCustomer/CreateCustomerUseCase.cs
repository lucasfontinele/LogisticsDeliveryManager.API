using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;
using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Domain.Repositories.Customers;
using LogisticsDeliveryManager.Domain.Services.Customers;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Application.UseCases.Customer.CreateCustomer
{
    public class CreateCustomerUseCase : ICreateCustomerUseCase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerDomainService _customerDomainService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerUseCase(
            ICustomerRepository customerRepository, 
            ICustomerDomainService customerDomainService,
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _customerDomainService = customerDomainService;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateCustomerResponseJson> Execute(CreateCustomerRequestJson request)
        {
            Validate(request);

            await _customerDomainService.ValidateUniqueEmail(request.Email);

            var addresses = request.Addresses.Select(a => new Address(
                a.Street,
                a.City,
                a.State,
                a.PostalCode
            )).ToList();

            var customer = new Domain.Entities.Customer(
                request.Name,
                request.Document,
                request.PhoneNumber,
                request.Email,
                (CustomerType)request.CustomerType,
                addresses
            );

            await _customerRepository.Add(customer);

            await _unitOfWork.Commit();

            return new CreateCustomerResponseJson
            {
                Id = customer.Id,
                Addresses = request.Addresses,
                CustomerType = request.CustomerType,
                Document = request.Document,
                Email = request.Email,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
            };
        }

        private void Validate(CreateCustomerRequestJson request)
        {
            var result = new CreateCustomerValidator().Validate(request);

            if (result.IsValid.Equals(false))
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
