using LogisticsDeliveryManager.Exception.ExceptionsBase;

namespace LogisticsDeliveryManager.Domain.Services.Drivers;

public class DriverJourneyPolicy : IDriverJourneyPolicy
{
    private static readonly TimeSpan MaximumJourneyDuration = TimeSpan.FromHours(10);
    private static readonly TimeSpan MaximumDailyAccumulatedDuration = TimeSpan.FromHours(11);

    public void ValidateJourney(TimeSpan plannedJourneyDuration, TimeSpan dailyDrivenDuration)
    {
        var errors = new List<string>();

        if (plannedJourneyDuration <= TimeSpan.Zero)
            errors.Add("Planned journey duration must be greater than zero.");

        if (plannedJourneyDuration > MaximumJourneyDuration)
            errors.Add($"Planned journey cannot exceed {MaximumJourneyDuration.TotalHours} hours.");

        if (dailyDrivenDuration < TimeSpan.Zero)
            errors.Add("Daily driven duration cannot be negative.");

        if (dailyDrivenDuration + plannedJourneyDuration > MaximumDailyAccumulatedDuration)
            errors.Add($"Driver daily journey limit of {MaximumDailyAccumulatedDuration.TotalHours} hours has been exceeded.");

        if (errors.Any())
            throw new ErrorOnValidationException(errors);
    }
}
