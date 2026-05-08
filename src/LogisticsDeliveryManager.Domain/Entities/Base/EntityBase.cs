namespace LogisticsDeliveryManager.Domain.Entities.Base
{
    public class EntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; private set; } = true;

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
