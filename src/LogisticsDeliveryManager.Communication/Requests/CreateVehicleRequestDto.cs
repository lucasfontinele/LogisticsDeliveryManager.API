using LogisticsDeliveryManager.Communication.Enums;

namespace LogisticsDeliveryManager.Communication.Requests
{
    public class Create3VehicleRequestDto
    {
        public required string LicensePlate { get; set; }
        public required string Model { get; set; }
        public required double WeightCapacity { get; set; }
        public required double VolumeCapacity { get; set; }
        public required CompartmentTypeDto CompartmentType { get; set; }
    }
}