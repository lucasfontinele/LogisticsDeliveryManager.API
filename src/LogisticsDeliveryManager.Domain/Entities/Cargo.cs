using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Domain.Entities
{
    public class Cargo
    {
        public long Id { get; set; }
        public double Weight { get; set; }
        public double Volume { get; set; }
        public CargoType Type { get; set; }
    }
}