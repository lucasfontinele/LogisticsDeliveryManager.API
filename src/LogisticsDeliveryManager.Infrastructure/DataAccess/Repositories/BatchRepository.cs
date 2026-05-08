using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Batches;
using Microsoft.EntityFrameworkCore;

namespace LogisticsDeliveryManager.Infrastructure.DataAccess.Repositories
{
    internal class BatchRepository : IBatchRepository
    {
        private readonly LogisticsDeliveryManagerDbContext _dbContext;

        public BatchRepository(LogisticsDeliveryManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Batch batch)
        {
            await _dbContext.AddAsync(batch);
        }

        public async Task<Batch?> GetById(long id)
        {
            return await _dbContext.Batches
                .Include(b => b.Driver)
                .Include(b => b.Vehicle)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Batch>> GetAll()
        {
            return await _dbContext.Batches
                .Include(b => b.Driver)
                .Include(b => b.Vehicle)
                .ToListAsync();
        }

        public void Update(Batch batch)
        {
            _dbContext.Batches.Update(batch);
        }

        public void Delete(Batch batch)
        {
            batch.Deactivate();
            _dbContext.Batches.Update(batch);
        }
    }
}
