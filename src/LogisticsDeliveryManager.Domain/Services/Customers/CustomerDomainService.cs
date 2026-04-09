using LogisticsDeliveryManager.Domain.Repositories.Customers;
using LogisticsDeliveryManager.Domain.ValueObjects;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Services.Customers;

public class CustomerDomainService : ICustomerDomainService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerDomainService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task ValidateUniqueEmail(Email email)
    {
        var emailExist = await _customerRepository.ExistActiveCustomerWithEmail(email.Address);
        if (emailExist)
        {
            throw new ErrorOnValidationException(new List<string> { "User with this e-mail already registered" });
        }
    }
}
