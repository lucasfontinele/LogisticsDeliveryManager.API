using LogisticsDeliveryManager.Communication.Enums;

namespace LogisticsDeliveryManager.Communication.Responses
{
    public class CreateVehicleResponseJson
    {
        public Guid Id { get; set; }
        public required string LicensePlate { get; set; }
        public required string Model { get; set; }
        public required double WeightCapacity { get; set; }
        public required double VolumeCapacity { get; set; }
        public required CompartmentTypeJson CompartmentType { get; set; }
    }
}
