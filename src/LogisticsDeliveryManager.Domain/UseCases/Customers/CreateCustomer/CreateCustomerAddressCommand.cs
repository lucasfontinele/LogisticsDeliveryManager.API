namespace LogisticsDeliveryManager.Domain.UseCases.Customers.CreateCustomer;

public sealed record CreateCustomerAddressCommand(
    string Street,
    string City,
    string State,
    string PostalCode);
