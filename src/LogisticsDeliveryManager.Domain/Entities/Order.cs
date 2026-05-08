
using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Exception.ExceptionsBase;
using System.Collections.Generic;
using System.Linq;

using LogisticsDeliveryManager.Domain.Entities.Base;

namespace LogisticsDeliveryManager.Domain.Entities;
public class Order : EntityBase
{
    public Customer Customer { get; private set; }
    public OrderStatus Status { get; private set; }
    public Address DestinationAddress { get; private set; }
    public DateTime DeliveryWindowStart { get; private set; }
    public DateTime DeliveryWindowEnd { get; private set; }
    public CargoType CargoType { get; private set; }
    public double Weight { get; private set; }
    public double Volume { get; private set; }
    public bool IsPriority { get; private set; }
    public string? DeliveryProofImageBase64 { get; private set; }
    public Vehicle? AssignedVehicle { get; private set; }

    protected Order() { }

    public Order(Customer customer, Address destinationAddress, DateTime deliveryWindowStart, DateTime deliveryWindowEnd, CargoType cargoType, double weight, double volume, bool isPriority)
    {
        Validate(customer, destinationAddress, deliveryWindowStart, deliveryWindowEnd, weight, volume);
        
        Customer = customer;
        DestinationAddress = destinationAddress;
        DeliveryWindowStart = deliveryWindowStart;
        DeliveryWindowEnd = deliveryWindowEnd;
        CargoType = cargoType;
        Weight = weight;
        Volume = volume;
        IsPriority = isPriority;
        Status = OrderStatus.Pending; // Initial status
    }

    public void AssignVehicle(Vehicle vehicle)
    {
        AssignedVehicle = vehicle;
        Status = OrderStatus.Processing; // Assigned but not embarked yet
    }

    public void SetProofOfDelivery(string base64Image)
    {
        DeliveryProofImageBase64 = base64Image;
        Status = OrderStatus.Delivered;
    }

    public void UpdateStatus(OrderStatus status)
    {
        Status = status;
    }

    private static void Validate(Customer customer, Address destinationAddress, DateTime deliveryWindowStart, DateTime deliveryWindowEnd, double weight, double volume)
    {
        var errors = new List<string>();

        if (customer == null)
            errors.Add("Customer cannot be null.");

        if (destinationAddress == null)
            errors.Add("Destination address cannot be null.");

        if (deliveryWindowStart >= deliveryWindowEnd)
            errors.Add("Delivery window start must be before delivery window end.");

        if (weight <= 0)
            errors.Add("Weight must be greater than zero.");

        if (volume <= 0)
            errors.Add("Volume must be greater than zero.");

        if (errors.Any())
            throw new ErrorOnValidationException(errors);
    }
}
