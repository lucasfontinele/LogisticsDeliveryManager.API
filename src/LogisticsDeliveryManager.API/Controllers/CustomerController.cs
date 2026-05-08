using LogisticsDeliveryManager.Communication.Enums;
using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;
using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Application.UseCases.Customers.CreateCustomer;
using LogisticsDeliveryManager.Application.UseCases.Customers.GetAllCustomers;
using LogisticsDeliveryManager.Application.UseCases.Customers.GetCustomerById;
using LogisticsDeliveryManager.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsDeliveryManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CreateCustomerResponseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCustomer(
        [FromServices] ICreateCustomerUseCase useCase,
        [FromServices] AutoMapper.IMapper mapper,
        [FromBody] CreateCustomerRequestJson request)
    {
        if (request is null)
            throw new ErrorOnValidationException(["Request cannot be null."]);

        var command = new CreateCustomerCommand(
            request.Name,
            request.Document,
            request.Addresses?.Select(address => new CreateCustomerAddressCommand(
                address.Street,
                address.City,
                address.State,
                address.PostalCode)).ToList()!,
            (CustomerType)request.CustomerType,
            request.PhoneNumber,
            request.Email);

        var customer = await useCase.Execute(command);

        var response = mapper.Map<CreateCustomerResponseJson>(customer);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CustomerResponseJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCustomers(
        [FromServices] IGetAllCustomersUseCase useCase,
        [FromServices] AutoMapper.IMapper mapper)
    {
        var customers = await useCase.Execute();
        var response = mapper.Map<IEnumerable<CustomerResponseJson>>(customers);
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CustomerResponseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCustomerById(
        [FromServices] IGetCustomerByIdUseCase useCase,
        [FromServices] AutoMapper.IMapper mapper,
        [FromRoute] Guid id)
    {
        var customer = await useCase.Execute(id);
        if (customer is null) return NotFound();
        var response = mapper.Map<CustomerResponseJson>(customer);
        return Ok(response);
    }
}
