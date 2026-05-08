using LogisticsDeliveryManager.Communication.Enums;

namespace LogisticsDeliveryManager.Communication.Responses
{
    public class CreateBatchResponseJson
    {
        public long Id { get; set; }
        public CargoTypeJson Type { get; set; }
        public long DriverId { get; set; }
        public long VehicleId { get; set; }
        public DateOnly DeliveryDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
