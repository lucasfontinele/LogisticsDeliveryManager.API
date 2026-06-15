using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Domain.Services.Orders;
using LogisticsDeliveryManager.Domain.ValueObjects;

namespace LogisticsDeliveryManager.Domain.Factories;

public class OrderFactory
{
    private readonly IDeliveryPromiseService _deliveryPromiseService;

    public OrderFactory(IDeliveryPromiseService deliveryPromiseService)
    {
        _deliveryPromiseService = deliveryPromiseService;
    }

    public Order Create(
        Guid customerId,
        string street,
        string city,
        string state,
        string postalCode,
        CargoType cargoType,
        double weight,
        double volume,
        bool isPriority)
    {
        var destinationAddress = new Address(
            street,
            city,
            state,
            new PostalCode(postalCode));

        var deliveryWindow = _deliveryPromiseService.CalculateDeliveryPromise(isPriority);
        var weightVo = new Weight(weight);
        var volumeVo = new Volume(volume);

        return new Order(
            customerId,
            destinationAddress,
            deliveryWindow,
            cargoType,
            weightVo,
            volumeVo,
            isPriority);
    }
}
