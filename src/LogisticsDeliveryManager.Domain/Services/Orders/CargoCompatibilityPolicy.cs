using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Domain.Services.Orders;

public class CargoCompatibilityPolicy : ICargoCompatibilityPolicy
{
    private static readonly CargoType[] DangerousIncompatibleTypes =
    {
        CargoType.Food,
        CargoType.Medicine,
        CargoType.Fragile,
        CargoType.HighValue,
        CargoType.Documents
    };

    public bool IsVehicleCompatible(CargoType cargoType, CompartmentType compartmentType)
    {
        if (cargoType == CargoType.Refrigerated || cargoType == CargoType.Medicine)
            return compartmentType == CompartmentType.RefrigeratedBody;

        if (cargoType == CargoType.Dangerous)
            return compartmentType != CompartmentType.RefrigeratedBody;

        return true;
    }

    public bool CanMixCargoTypes(CargoType firstCargoType, CargoType secondCargoType)
    {
        if (firstCargoType == CargoType.Dangerous && DangerousIncompatibleTypes.Contains(secondCargoType))
            return false;

        if (secondCargoType == CargoType.Dangerous && DangerousIncompatibleTypes.Contains(firstCargoType))
            return false;

        return true;
    }
}
