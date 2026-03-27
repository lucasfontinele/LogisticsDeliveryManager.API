using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;

namespace LogisticsDeliveryManager.Application.UseCases.Customer.CreateCustomer
{
    public interface ICreateCustomerUseCase
    {
        public Task<CreateCustomerResponseDto> Execute(CreateCustomerRequestDto request);
    }
}
