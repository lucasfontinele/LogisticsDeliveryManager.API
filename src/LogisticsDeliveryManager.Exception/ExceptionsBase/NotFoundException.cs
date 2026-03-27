using System.Net;

namespace LogisticsDeliveryManager.Exception.ExceptionsBase;

public class NotFoundException : LogisticsDeliveryManagerException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}