using AutoMapper;
using LogisticsDeliveryManager.Application.UseCases.Orders.CreateOrder;
using LogisticsDeliveryManager.Communication.Requests;
using LogisticsDeliveryManager.Communication.Responses;
using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Exception.ExceptionsBase;
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

    [HttpGet("customer/{customerId}")]
    [ProducesResponseType(typeof(IEnumerable<OrderResponseJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCustomerOrders(
        [FromServices] LogisticsDeliveryManager.Application.UseCases.Orders.GetCustomerOrders.IGetCustomerOrdersUseCase useCase,
        [FromServices] IMapper mapper,
        [FromRoute] long customerId)
    {
        var orders = await useCase.Execute(customerId);
        var response = mapper.Map<IEnumerable<OrderResponseJson>>(orders);
        return Ok(response);
    }

    [HttpGet("driver/{driverId}")]
    [ProducesResponseType(typeof(IEnumerable<OrderResponseJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDriverOrders(
        [FromServices] LogisticsDeliveryManager.Application.UseCases.Orders.GetDriverOrders.IGetDriverOrdersUseCase useCase,
        [FromServices] IMapper mapper,
        [FromRoute] long driverId)
    {
        var orders = await useCase.Execute(driverId);
        var response = mapper.Map<IEnumerable<OrderResponseJson>>(orders);
        return Ok(response);
    }

    [HttpPatch("{id}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateStatus(
        [FromServices] LogisticsDeliveryManager.Application.UseCases.Orders.UpdateStatus.IUpdateOrderStatusUseCase useCase,
        [FromRoute] long id,
        [FromBody] OrderStatus status)
    {
        var command = new LogisticsDeliveryManager.Application.UseCases.Orders.UpdateStatus.UpdateOrderStatusCommand(id, status);
        await useCase.Execute(command);
        return NoContent();
    }

    [HttpPost("{id}/proof")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UploadProof(
        [FromServices] LogisticsDeliveryManager.Application.UseCases.Orders.UploadProof.IUploadOrderProofUseCase useCase,
        [FromRoute] long id,
        [FromBody] string base64Image)
    {
        var command = new LogisticsDeliveryManager.Application.UseCases.Orders.UploadProof.UploadOrderProofCommand(id, base64Image);
        await useCase.Execute(command);
        return NoContent();
    }

    [HttpPost("{id}/evaluate")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Evaluate(
        [FromServices] LogisticsDeliveryManager.Application.UseCases.Orders.Evaluate.IEvaluateOrderUseCase useCase,
        [FromRoute] long id,
        [FromBody] EvaluateRequestJson request)
    {
        var command = new LogisticsDeliveryManager.Application.UseCases.Orders.Evaluate.EvaluateOrderCommand(id, request.Rating, request.Feedback);
        await useCase.Execute(command);
        return NoContent();
    }
}
