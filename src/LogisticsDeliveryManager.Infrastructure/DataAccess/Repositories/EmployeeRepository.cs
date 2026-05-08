using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Employees;
using Microsoft.EntityFrameworkCore;

namespace LogisticsDeliveryManager.Infrastructure.DataAccess.Repositories;

internal class EmployeeRepository : IEmployeeRepository
{
    private readonly LogisticsDeliveryManagerDbContext _dbContext;

    public EmployeeRepository(LogisticsDeliveryManagerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Employee employee)
    {
        await _dbContext.AddAsync(employee);
    }

    public async Task<Employee?> GetById(Guid id)
    {
        return await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<Employee>> GetAll()
    {
        return await _dbContext.Employees.ToListAsync();
    }

    public void Update(Employee employee)
    {
        _dbContext.Employees.Update(employee);
    }

    public void Delete(Employee employee)
    {
        employee.Deactivate();
        _dbContext.Employees.Update(employee);
    }

    public async Task<bool> ExistActiveEmployeeWithEmail(string email)
    {
        return await _dbContext.Employees.AnyAsync(e => e.Email == email);
    }
}
