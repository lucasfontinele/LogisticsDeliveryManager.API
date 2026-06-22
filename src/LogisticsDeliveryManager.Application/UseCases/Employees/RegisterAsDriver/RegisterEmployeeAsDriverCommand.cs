using LogisticsDeliveryManager.Domain.Enums;

namespace LogisticsDeliveryManager.Application.UseCases.Employees.RegisterAsDriver;

public class RegisterEmployeeAsDriverCommand
{
    public Guid EmployeeId { get; }
    public IEnumerable<DriverLicenseType> LicenseTypes { get; }

    public RegisterEmployeeAsDriverCommand(Guid employeeId, IEnumerable<DriverLicenseType> licenseTypes)
    {
        EmployeeId = employeeId;
        LicenseTypes = licenseTypes;
    }
}
