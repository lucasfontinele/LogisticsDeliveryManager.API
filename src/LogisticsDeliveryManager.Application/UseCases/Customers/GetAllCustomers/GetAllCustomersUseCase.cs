using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Customers;

namespace LogisticsDeliveryManager.Application.UseCases.Customers.GetAllCustomers;

public interface IGetAllCustomersUseCase
{
    Task<IEnumerable<Customer>> Execute();
}

public class GetAllCustomersUseCase : IGetAllCustomersUseCase
{
    private readonly ICustomerRepository _repository;

    public GetAllCustomersUseCase(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Customer>> Execute()
    {
        return await _repository.GetAll();
    }
}
