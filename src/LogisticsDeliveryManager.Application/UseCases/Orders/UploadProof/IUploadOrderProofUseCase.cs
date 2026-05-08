namespace LogisticsDeliveryManager.Application.UseCases.Orders.UploadProof;

public class UploadOrderProofCommand
{
    public long OrderId { get; set; }
    public string ProofImageBase64 { get; set; } = string.Empty;

    public UploadOrderProofCommand(long orderId, string proofImageBase64)
    {
        OrderId = orderId;
        ProofImageBase64 = proofImageBase64;
    }
}

public interface IUploadOrderProofUseCase
{
    Task Execute(UploadOrderProofCommand command);
}
