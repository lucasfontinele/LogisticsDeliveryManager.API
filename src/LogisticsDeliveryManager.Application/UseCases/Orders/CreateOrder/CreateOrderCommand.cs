using LogisticsDeliveryManager.Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace LogisticsDeliveryManager.Application.UseCases.Orders.CreateOrder;

public class CreateOrderCommand
{
    public Guid CustomerId { get; set; }
    public required CreateOrderAddressCommand DestinationAddress { get; set; }
    public CargoType CargoType { get; set; }
    public double Weight { get; set; }
    public double Volume { get; set; }
    public bool IsPriority { get; set; }

    [SetsRequiredMembers]
    public CreateOrderCommand(Guid customerId, CreateOrderAddressCommand destinationAddress, CargoType cargoType, double weight, double volume, bool isPriority)
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
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string PostalCode { get; set; }

    [SetsRequiredMembers]
    public CreateOrderAddressCommand(string street, string city, string state, string postalCode)
    {
        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
    }
}
