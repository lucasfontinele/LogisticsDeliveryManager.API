using LogisticsDeliveryManager.Domain.Repositories.Employees;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Services.Employees;

public class EmployeeDomainService : IEmployeeDomainService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeDomainService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task ValidateUniqueEmail(string email)
    {
        var exist = await _repository.ExistActiveEmployeeWithEmail(email);
        if (exist)
        {
            throw new ErrorOnValidationException(["An employee with this email already exists."]);
        }
    }
}
