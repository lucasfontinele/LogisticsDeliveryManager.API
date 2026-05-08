using LogisticsDeliveryManager.Communication.Enums;

namespace LogisticsDeliveryManager.Communication.Requests
{
    public class CreateBatchRequestJson  
    {
        public CargoTypeJson Type { get; set; }
        public long DriverId { get; set; }
        public long VehicleId { get; set; }
        public DateOnly DeliveryDate { get; set; }
    }
}
