namespace LogisticsDeliveryManager.Domain.Services.Employees;

public interface IEmployeeDomainService
{
    Task ValidateUniqueEmail(string email);
}
