using LogisticsDeliveryManager.Communication.Enums;

namespace LogisticsDeliveryManager.Communication.Responses
{
    public class CreateBatchResponseJson
    {
        public Guid Id { get; set; }
        public CargoTypeJson Type { get; set; }
        public Guid DriverId { get; set; }
        public Guid VehicleId { get; set; }
        public DateOnly DeliveryDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
