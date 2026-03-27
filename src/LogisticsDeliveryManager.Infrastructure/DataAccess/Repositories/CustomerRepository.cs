using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Customers;
using Microsoft.EntityFrameworkCore;

namespace LogisticsDeliveryManager.Infrastructure.DataAccess.Repositories
{
    internal class CustomerRepository : ICustomerRepository
    {
        private readonly LogisticsDeliveryManagerDbContext dbContext;

        public CustomerRepository(LogisticsDeliveryManagerDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task Add(Customer customer)
        {
            await dbContext.AddAsync(customer);

            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistActiveCustomerWithEmail(string email)
        {
            return await dbContext.Customers.AnyAsync(c => c.Email == email);
        }
    }
}
