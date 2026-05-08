using AutoMapper;
using LogisticsDeliveryManager.Application.UseCases.Orders.CreateOrder;
using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;
using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsDeliveryManager.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(OrderResponseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponseJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder(
        [FromServices] ICreateOrderUseCase useCase,
        [FromServices] IMapper mapper,
        [FromBody] CreateOrderRequestJson request)
    {
        if (request is null)
            throw new ErrorOnValidationException(["Request cannot be null."]);

        var command = new CreateOrderCommand(
            request.CustomerId,
            new CreateOrderAddressCommand(
                request.DestinationAddress.Street,
                request.DestinationAddress.City,
                request.DestinationAddress.State,
                request.DestinationAddress.PostalCode),
            (CargoType)request.CargoType,
            request.Weight,
            request.Volume,
            request.IsPriority);

        var order = await useCase.Execute(command);

        var response = mapper.Map<OrderResponseJson>(order);

        return Created(string.Empty, response);
    }
}
