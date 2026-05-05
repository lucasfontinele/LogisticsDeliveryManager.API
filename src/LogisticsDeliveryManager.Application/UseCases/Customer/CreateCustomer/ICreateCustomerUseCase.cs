using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;

namespace LogisticsDeliveryManager.Application.UseCases.Customer.CreateCustomer
{
    public interface ICreateCustomerUseCase
    {
        public Task<CreateCustomerResponseJson> Execute(CreateCustomerRequestJson request);
    }
}
