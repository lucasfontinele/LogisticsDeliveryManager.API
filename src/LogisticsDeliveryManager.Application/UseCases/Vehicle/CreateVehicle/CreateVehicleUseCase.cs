using VehicleEntity = LogisticsDeliveryManager.Domain.Entities.Vehicle;
using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Domain.Repositories.Vehicles;
using LogisticsDeliveryManager.Domain.Services.Vehicles;
using LogisticsDeliveryManager.Domain.Repositories;
using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Application.UseCases.Vehicle.CreateVehicle
{
    public class CreateVehicleUseCase : ICreateVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehicleDomainService _vehicleDomainService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateVehicleUseCase(
            IVehicleRepository vehicleRepository,
            IVehicleDomainService vehicleDomainService,
            IUnitOfWork unitOfWork)
        {
            _vehicleRepository = vehicleRepository;
            _vehicleDomainService = vehicleDomainService;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateVehicleResponseJson> Execute(CreateVehicleRequestJson request)
        {
            Validate(request);

            var vehicle = new VehicleEntity(
                request.LicensePlate,
                request.Model,
                request.WeightCapacity,
                request.VolumeCapacity,
                (CompartmentType)request.CompartmentType
            );

            await _vehicleRepository.Add(vehicle);

            await _unitOfWork.Commit();

            return new CreateVehicleResponseJson
            {
                Id = vehicle.Id,
                LicensePlate = request.LicensePlate,
                Model = request.Model,
                WeightCapacity = request.WeightCapacity,
                VolumeCapacity = request.VolumeCapacity,
                CompartmentType = request.CompartmentType
            };
        }

        private void Validate(CreateVehicleRequestJson request)
        {
            var result = new CreateVehicleValidator().Validate(request);

            if (result.IsValid.Equals(false))
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
