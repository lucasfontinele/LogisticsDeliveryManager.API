﻿namespace LogisticsDeliveryManager.Domain.Entities.Base
{
    public class EntityBase
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
