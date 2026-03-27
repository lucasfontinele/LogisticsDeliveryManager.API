using Microsoft.AspNetCore.Routing;

namespace LogisticsDeliveryManager.API.Routing;

public sealed class LowercaseParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        return value?.ToString()?.ToLowerInvariant();
    }
}
