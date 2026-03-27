namespace LogisticsDeliveryManager.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}
