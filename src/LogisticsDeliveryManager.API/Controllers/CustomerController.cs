using LogisticsDeliveryManager.Communication.Enums;
using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;
using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Application.UseCases.Customers.CreateCustomer;
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
}
