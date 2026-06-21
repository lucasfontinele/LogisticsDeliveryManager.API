using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Domain.Entities.Base;
using LogisticsDeliveryManager.Domain.ValueObjects;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Entities;

public class Employee : Person
{
    public RoleType RoleType { get; private set; }
    public IEnumerable<DriverLicenseType> LicenseTypes { get; private set; } = Array.Empty<DriverLicenseType>();

    private Employee() { }

    public Employee(string name, string document, string phoneNumber, Email email, RoleType roleType)
        : base(name, document, phoneNumber, email)
    {
        Validate(roleType);
        
        RoleType = roleType;
    }

    public void RegisterAsDriver(IEnumerable<DriverLicenseType>? licenseTypes)
    {
        var licenseTypesList = licenseTypes?.ToList() ?? [];

        if (licenseTypesList.Count == 0)
            throw new ErrorOnValidationException(["At least one driver license type must be provided."]);

        RoleType = RoleType.Driver;
        LicenseTypes = licenseTypesList;
    }

    private static void Validate(RoleType roleType)
    {
        var errors = new List<string>();

        if (!Enum.IsDefined(typeof(RoleType), roleType))
            errors.Add("Invalid role type.");

        if (errors.Any())
            throw new ErrorOnValidationException(errors);
    }
}
