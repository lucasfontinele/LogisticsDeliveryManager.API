using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Application.UseCases.Customers.CreateCustomer;

public sealed record CreateCustomerCommand(
    string Name,
    string Document,
    IReadOnlyCollection<CreateCustomerAddressCommand> Addresses,
    CustomerType CustomerType,
    string PhoneNumber,
    string Email);
