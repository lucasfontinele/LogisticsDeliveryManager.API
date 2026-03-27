namespace LogisticsDeliveryManager.Communication.Responses;

public class ErrorResponseDto
{
    public List<string> ErrorMessages { get; set; }

    public ErrorResponseDto(string errorMessage)
    {
        ErrorMessages = [errorMessage];
    }

    public ErrorResponseDto(List<string> errorMessage)
    {
        ErrorMessages = errorMessage;
    }
}