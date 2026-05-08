using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Domain.Repositories.Customers;
using LogisticsDeliveryManager.Domain.Repositories.Orders;
using LogisticsDeliveryManager.Domain.Repositories.Vehicles;
using LogisticsDeliveryManager.Domain.Services.Orders;
using LogisticsDeliveryManager.Domain.ValueObjects;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Application.UseCases.Orders.CreateOrder;

public class CreateOrderUseCase : ICreateOrderUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IOrderRoutingDomainService _routingService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderUseCase(
        IOrderRepository orderRepository,
        ICustomerRepository customerRepository,
        IVehicleRepository vehicleRepository,
        IOrderRoutingDomainService routingService,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _customerRepository = customerRepository;
        _vehicleRepository = vehicleRepository;
        _routingService = routingService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Order> Execute(CreateOrderCommand command)
    {
        var customer = await _customerRepository.GetById(command.CustomerId);
        if (customer is null)
            throw new ErrorOnValidationException(["Customer not found."]);

        var sla = _routingService.CalculateSla(command.IsPriority);

        var destinationAddress = new Address(
            command.DestinationAddress.Street,
            command.DestinationAddress.City,
            command.DestinationAddress.State,
            new PostalCode(command.DestinationAddress.PostalCode));

        var order = new Order(
            customer,
            destinationAddress,
            sla.Start,
            sla.End,
            command.CargoType,
            command.Weight,
            command.Volume,
            command.IsPriority);

        var vehicles = await _vehicleRepository.GetAll();
        var bestVehicle = _routingService.FindBestVehicleForOrder(order, vehicles);

        if (bestVehicle is not null)
        {
            order.AssignVehicle(bestVehicle);
            // In a real scenario, we might want to update vehicle status if it becomes full
            // For now, we just link them
        }

        await _orderRepository.Add(order);
        await _unitOfWork.Commit();

        return order;
    }
}
