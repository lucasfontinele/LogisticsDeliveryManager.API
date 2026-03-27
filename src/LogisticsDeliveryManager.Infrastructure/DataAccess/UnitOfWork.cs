using LogisticsDeliveryManager.Domain.Repositories;

namespace LogisticsDeliveryManager.Infrastructure.DataAccess
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly LogisticsDeliveryManagerDbContext _dbContext;

        public UnitOfWork(LogisticsDeliveryManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit() => await _dbContext.SaveChangesAsync();
    }
}
