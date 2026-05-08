using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Fleet.Drivers;

public class Driver
{
    private readonly List<DriverLicenseCategory> _licenseCategories = new();

    public long Id { get; private set; }
    public string Name { get; private set; }
    public TimeSpan DailyWorkLimit { get; private set; }
    public TimeSpan WorkedTimeToday { get; private set; }
    public Guid? CurrentRouteId { get; private set; }
    public IReadOnlyCollection<DriverLicenseCategory> LicenseCategories => _licenseCategories.AsReadOnly();

    private Driver()
    {
        Name = string.Empty;
    }

    public Driver(
        string name,
        IEnumerable<DriverLicenseCategory> licenseCategories,
        TimeSpan dailyWorkLimit)
    {
        Validate(name, licenseCategories, dailyWorkLimit);

        Name = name.Trim();
        DailyWorkLimit = dailyWorkLimit;
        WorkedTimeToday = TimeSpan.Zero;
        _licenseCategories.AddRange(licenseCategories.Distinct());
    }

    public bool HasLicenseFor(DriverLicenseCategory requiredCategory)
    {
        if (!Enum.IsDefined(typeof(DriverLicenseCategory), requiredCategory))
            throw new ErrorOnValidationException(["Invalid driver license category."]);

        return _licenseCategories.Any(category => Rank(category) >= Rank(requiredCategory));
    }

    public bool CanDriveFor(TimeSpan duration)
    {
        if (duration <= TimeSpan.Zero)
            return false;

        return WorkedTimeToday + duration <= DailyWorkLimit;
    }

    public void AssignToRoute(Guid routeId, TimeSpan plannedDrivingTime)
    {
        var errors = new List<string>();

        if (routeId == Guid.Empty)
            errors.Add("Route id cannot be empty.");

        if (plannedDrivingTime <= TimeSpan.Zero)
            errors.Add("Planned driving time must be greater than zero.");

        if (CurrentRouteId.HasValue && CurrentRouteId.Value != routeId)
            errors.Add("Driver is already assigned to another route.");

        if (!CanDriveFor(plannedDrivingTime))
            errors.Add("Driver cannot exceed the daily work limit.");

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);

        if (CurrentRouteId.HasValue && CurrentRouteId.Value == routeId)
            return;

        CurrentRouteId = routeId;
        RegisterWorkedTime(plannedDrivingTime);
    }

    public void RegisterWorkedTime(TimeSpan workedTime)
    {
        if (workedTime <= TimeSpan.Zero)
            throw new ErrorOnValidationException(["Worked time must be greater than zero."]);

        if (!CanDriveFor(workedTime))
            throw new ErrorOnValidationException(["Driver cannot exceed the daily work limit."]);

        WorkedTimeToday += workedTime;
    }

    public void ReleaseFromRoute(Guid routeId)
    {
        if (!CurrentRouteId.HasValue || CurrentRouteId.Value != routeId)
            throw new ErrorOnValidationException(["Driver is not assigned to the informed route."]);

        CurrentRouteId = null;
    }

    public void ResetDailyWorkload()
    {
        WorkedTimeToday = TimeSpan.Zero;
        CurrentRouteId = null;
    }

    private static void Validate(
        string name,
        IEnumerable<DriverLicenseCategory> licenseCategories,
        TimeSpan dailyWorkLimit)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(name))
            errors.Add("Driver name cannot be empty.");

        if (licenseCategories is null || !licenseCategories.Any())
            errors.Add("At least one driver license category must be provided.");

        if (dailyWorkLimit <= TimeSpan.Zero)
            errors.Add("Daily work limit must be greater than zero.");

        if (errors.Count > 0)
            throw new ErrorOnValidationException(errors);
    }

    private static int Rank(DriverLicenseCategory category)
    {
        return category switch
        {
            DriverLicenseCategory.A => 1,
            DriverLicenseCategory.B => 2,
            DriverLicenseCategory.C => 3,
            DriverLicenseCategory.D => 4,
            DriverLicenseCategory.E => 5,
            _ => throw new ErrorOnValidationException(["Invalid driver license category."])
        };
    }
}
