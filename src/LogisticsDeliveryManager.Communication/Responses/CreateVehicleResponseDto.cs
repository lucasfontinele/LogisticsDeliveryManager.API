using LogisticsDeliveryManager.Communication.Enums;

namespace LogisticsDeliveryManager.Communication.Responses
{
    public class CreateVehicleResponseDto
    {
        public long Id { get; set; }
        public string LicensePlate { get; set; }
        public string Model { get; set; }
        public double WeightCapacity { get; set; }
        public double VolumeCapacity { get; set; }
        public CompartmentTypeDto CompartmentType { get; set; }
    }
}