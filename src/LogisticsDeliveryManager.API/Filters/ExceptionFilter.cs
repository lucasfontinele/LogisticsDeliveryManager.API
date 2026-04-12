using LogisticsDeliveryManager.Communication.Responses;
using LogisticsDeliveryManager.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LogisticsDeliveryManager.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is LogisticsDeliveryManagerException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThrowUnkowError(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var logisticsDeliveryManagerException = (LogisticsDeliveryManagerException)context.Exception;
        var errorResponse = new ErrorResponseJson(logisticsDeliveryManagerException.GetErrors());

        context.HttpContext.Response.StatusCode = logisticsDeliveryManagerException.StatusCode;
        context.Result = new ObjectResult(errorResponse);
    }

    private void ThrowUnkowError(ExceptionContext context)
    {
        var errorResponse = new ErrorResponseJson("Unknown error");

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorResponse);
    }
}