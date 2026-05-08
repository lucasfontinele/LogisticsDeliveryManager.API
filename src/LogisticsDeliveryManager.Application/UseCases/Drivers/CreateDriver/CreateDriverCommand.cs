using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Application.UseCases.Drivers.CreateDriver;

public class CreateDriverCommand
{
    public Guid EmployeeId { get; private set; }
    public IEnumerable<DriverLicenseType> LicenseTypes { get; private set; }

    public CreateDriverCommand(Guid employeeId, IEnumerable<DriverLicenseType> licenseTypes)
    {
        EmployeeId = employeeId;
        LicenseTypes = licenseTypes;
    }
}
