using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Domain.Repositories.Customers;
using LogisticsDeliveryManager.Domain.Services.Customers;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Application.UseCases.Customers.CreateCustomer;

public sealed class CreateCustomerUseCase : ICreateCustomerUseCase
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

    public async Task<Customer> Execute(CreateCustomerCommand command)
    {
        Validate(command);

        await _customerDomainService.ValidateUniqueEmail(command.Email);

        var addresses = command.Addresses
            .Select(address => new Address(
                address.Street,
                address.City,
                address.State,
                address.PostalCode))
            .ToList();

        var customer = Customer.Register(
            command.Name,
            command.Document,
            command.PhoneNumber,
            command.Email,
            command.CustomerType,
            addresses);

        await _customerRepository.Add(customer);
        await _unitOfWork.Commit();

        return customer;
    }

    private static void Validate(CreateCustomerCommand? command)
    {
        if (command is null)
            throw new ErrorOnValidationException(["Request cannot be null."]);

        if (command.Addresses is null)
            throw new ErrorOnValidationException(["Addresses cannot be null."]);
    }
}
