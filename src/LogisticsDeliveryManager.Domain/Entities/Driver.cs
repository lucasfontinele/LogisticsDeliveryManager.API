using LogisticsDeliveryManager.Domain.Enums;
using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Entities;

public class Driver
{
    public long Id { get; set; }
    public IEnumerable<DriverLicenseType> LicenseTypes { get; set; }
    public Employee Employee { get; set; }

    public Driver(Employee employee, IEnumerable<DriverLicenseType> licenseTypes)
    {
        Employee = employee;
        LicenseTypes = licenseTypes;
    }

    private static void Validate(Employee employee, IEnumerable<DriverLicenseType> licenseTypes)
    {
        var errors = new List<string>();

        if (employee == null)
            errors.Add("Employee cannot be null.");

        if (licenseTypes == null || !licenseTypes.Any())
            errors.Add("At least one driver license type must be provided.");

        if (errors.Any())
            throw new ErrorOnValidationException(errors);
    }
}
