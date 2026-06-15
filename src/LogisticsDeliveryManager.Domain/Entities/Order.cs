
using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Exception.ExceptionsBase;
using LogisticsDeliveryManager.Domain.Entities.Base;
using LogisticsDeliveryManager.Domain.ValueObjects;

namespace LogisticsDeliveryManager.Domain.Entities;
public class Order : EntityBase
{
    public Guid CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public Address DestinationAddress { get; private set; }
    public DeliveryWindow DeliveryWindow { get; private set; }
    public CargoType CargoType { get; private set; }
    public Weight Weight { get; private set; }
    public Volume Volume { get; private set; }
    public bool IsPriority { get; private set; }
    public string? DeliveryProofImageBase64 { get; private set; }
    public Guid? AssignedVehicleId { get; private set; }
    public int? Rating { get; private set; }
    public string? Feedback { get; private set; }

    protected Order() { }

    public Order(Guid customerId, Address destinationAddress, DeliveryWindow deliveryWindow, CargoType cargoType, Weight weight, Volume volume, bool isPriority)
    {
        Validate(customerId, destinationAddress, deliveryWindow, weight, volume);

        CustomerId = customerId;
        DestinationAddress = destinationAddress;
        DeliveryWindow = deliveryWindow;
        CargoType = cargoType;
        Weight = weight;
        Volume = volume;
        IsPriority = isPriority;
        Status = OrderStatus.Pending;
    }

    public void AssignVehicle(Guid vehicleId)
    {
        AssignedVehicleId = vehicleId;
    }

    public void SetProofOfDelivery(string base64Image)
    {
        DeliveryProofImageBase64 = base64Image;
        Status = OrderStatus.Delivered;
    }

    public void ConfirmDelivery()
    {
        if (Status == OrderStatus.Delivered)
            throw new ErrorOnValidationException(new List<string> { "Order is already delivered." });

        if (Status == OrderStatus.Cancelled)
            throw new ErrorOnValidationException(new List<string> { "Cancelled orders cannot be delivered." });

        Status = OrderStatus.Delivered;
    }

    public void CancelOrder()
    {
        if (Status == OrderStatus.Delivered)
            throw new ErrorOnValidationException(new List<string> { "Delivered orders cannot be cancelled." });

        if (Status == OrderStatus.Cancelled)
            throw new ErrorOnValidationException(new List<string> { "Order is already cancelled." });

        Status = OrderStatus.Cancelled;
    }

    public void RescheduleDelivery(DeliveryWindow newDeliveryWindow)
    {
        if (newDeliveryWindow == null)
            throw new ErrorOnValidationException(new List<string> { "Delivery window is required for rescheduling." });

        if (Status == OrderStatus.Delivered)
            throw new ErrorOnValidationException(new List<string> { "Delivered orders cannot be rescheduled." });

        if (Status == OrderStatus.Cancelled)
            throw new ErrorOnValidationException(new List<string> { "Cancelled orders cannot be rescheduled." });

        DeliveryWindow = newDeliveryWindow;
        Status = OrderStatus.Rescheduled;
    }

    public void Evaluate(int rating, string? feedback)
    {
        if (Status != OrderStatus.Delivered)
            throw new ErrorOnValidationException(["Only delivered orders can be evaluated."]);

        if (rating < 1 || rating > 5)
            throw new ErrorOnValidationException(["Rating must be between 1 and 5."]);

        Rating = rating;
        Feedback = feedback;
    }

    private static void Validate(Guid customerId, Address destinationAddress, DeliveryWindow deliveryWindow, Weight weight, Volume volume)
    {
        var errors = new List<string>();

        if (customerId == Guid.Empty)
            errors.Add("Customer id cannot be empty.");

        if (destinationAddress == null)
            errors.Add("Destination address cannot be null.");

        if (deliveryWindow == null)
            errors.Add("Delivery window cannot be null.");

        if (weight == null)
            errors.Add("Weight is required.");

        if (volume == null)
            errors.Add("Volume is required.");

        if (errors.Any())
            throw new ErrorOnValidationException(errors);
    }
}
