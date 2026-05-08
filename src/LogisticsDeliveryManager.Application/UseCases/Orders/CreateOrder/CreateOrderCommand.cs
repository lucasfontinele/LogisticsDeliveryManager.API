using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Application.UseCases.Orders.CreateOrder;

public class CreateOrderCommand
{
    public long CustomerId { get; set; }
    public CreateOrderAddressCommand DestinationAddress { get; set; } = null!;
    public CargoType CargoType { get; set; }
    public double Weight { get; set; }
    public double Volume { get; set; }
    public bool IsPriority { get; set; }

    public CreateOrderCommand(long customerId, CreateOrderAddressCommand destinationAddress, CargoType cargoType, double weight, double volume, bool isPriority)
    {
        CustomerId = customerId;
        DestinationAddress = destinationAddress;
        CargoType = cargoType;
        Weight = weight;
        Volume = volume;
        IsPriority = isPriority;
    }
}

public class CreateOrderAddressCommand
{
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;

    public CreateOrderAddressCommand(string street, string city, string state, string postalCode)
    {
        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
    }
}
