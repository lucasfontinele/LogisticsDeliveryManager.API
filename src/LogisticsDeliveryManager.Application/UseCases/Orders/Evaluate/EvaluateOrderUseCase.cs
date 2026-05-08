using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Domain.Repositories.Orders;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Application.UseCases.Orders.Evaluate;

public class EvaluateOrderUseCase : IEvaluateOrderUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EvaluateOrderUseCase(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(EvaluateOrderCommand command)
    {
        var order = await _orderRepository.GetById(command.OrderId);
        if (order is null)
            throw new ErrorOnValidationException(["Order not found."]);

        order.Evaluate(command.Rating, command.Feedback);

        _orderRepository.Update(order);
        await _unitOfWork.Commit();
    }
}
