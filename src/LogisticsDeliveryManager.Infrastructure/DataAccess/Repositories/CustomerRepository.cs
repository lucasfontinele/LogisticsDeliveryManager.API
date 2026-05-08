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
        }

        public async Task<Customer?> GetById(long id)
        {
            return await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await dbContext.Customers.ToListAsync();
        }

        public void Update(Customer customer)
        {
            dbContext.Customers.Update(customer);
        }

        public void Delete(Customer customer)
        {
            customer.Deactivate();
            dbContext.Customers.Update(customer);
        }

        public async Task<bool> ExistActiveCustomerWithEmail(string email)
        {
            return await dbContext.Customers.AnyAsync(c => c.Email == email);
        }
    }
}
