using LogisticsDeliveryManager.Domain.Entities;
using LogisticsDeliveryManager.Domain.Repositories.Customers;

namespace LogisticsDeliveryManager.Application.UseCases.Customers.GetCustomerById;

public interface IGetCustomerByIdUseCase
{
    Task<Customer?> Execute(long id);
}

public class GetCustomerByIdUseCase : IGetCustomerByIdUseCase
{
    private readonly ICustomerRepository _repository;

    public GetCustomerByIdUseCase(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Customer?> Execute(long id)
    {
        return await _repository.GetById(id);
    }
}
