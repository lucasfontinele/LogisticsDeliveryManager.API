using LogisticsDeliveryManager.Domain.ValueObjects;

namespace LogisticsDeliveryManager.Domain.Services.Customers;

public interface ICustomerDomainService
{
    Task ValidateUniqueEmail(Email email);
}
