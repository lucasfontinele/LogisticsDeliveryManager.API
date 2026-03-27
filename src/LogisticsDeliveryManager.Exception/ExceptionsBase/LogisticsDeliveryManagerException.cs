namespace LogisticsDeliveryManager.Exception.ExceptionsBase;

public abstract class LogisticsDeliveryManagerException : SystemException
{
    protected LogisticsDeliveryManagerException(string message) : base(message)
    {

    }

    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}