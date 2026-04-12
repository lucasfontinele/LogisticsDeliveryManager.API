namespace LogisticsDeliveryManager.Communication.Responses;

public class ErrorResponseJson
{
    public List<string> ErrorMessages { get; set; }

    public ErrorResponseJson(string errorMessage)
    {
        ErrorMessages = [errorMessage];
    }

    public ErrorResponseJson(List<string> errorMessage)
    {
        ErrorMessages = errorMessage;
    }
}
