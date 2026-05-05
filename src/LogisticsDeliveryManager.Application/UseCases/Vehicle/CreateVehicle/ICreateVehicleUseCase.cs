using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;

namespace LogisticsDeliveryManager.Application.UseCases.Vehicle.CreateVehicle
{
    public interface ICreateVehicleUseCase
    {
        public Task<CreateVehicleResponseJson> Execute(CreateVehicleRequestJson request);
    }
}
