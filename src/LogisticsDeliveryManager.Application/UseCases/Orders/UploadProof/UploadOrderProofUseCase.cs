using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Domain.Repositories.Orders;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Application.UseCases.Orders.UploadProof;

public class UploadOrderProofUseCase : IUploadOrderProofUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UploadOrderProofUseCase(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(UploadOrderProofCommand command)
    {
        var order = await _orderRepository.GetById(command.OrderId);
        if (order is null)
            throw new ErrorOnValidationException(["Order not found."]);

        order.SetProofOfDelivery(command.ProofImageBase64);

        _orderRepository.Update(order);
        await _unitOfWork.Commit();
    }
}
