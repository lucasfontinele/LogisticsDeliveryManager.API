using LogisticsDeliveryManager.Communication.Enums;

namespace LogisticsDeliveryManager.Communication.Requests
{
    public class CreateBatchRequestJson  
    {
        public CargoTypeJson Type { get; set; }
        public Guid DriverId { get; set; }
        public Guid VehicleId { get; set; }
        public DateOnly DeliveryDate { get; set; }
    }
}
