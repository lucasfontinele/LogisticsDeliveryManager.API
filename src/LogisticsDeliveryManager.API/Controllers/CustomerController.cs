using LogisticsDeliveryManager.Application.UseCases.Customer.CreateCustomer;
using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsDeliveryManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CreateCustomerResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCustomer([FromServices] ICreateCustomerUseCase useCase, [FromBody] CreateCustomerRequestDto request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}

