using System.Net;

namespace LogisticsDeliveryManager.Exception.ExceptionsBase;

public class InvalidLoginException : LogisticsDeliveryManagerException
{
    public InvalidLoginException() : base("E-mail e/ou senha inválidos.")
    {
    }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}