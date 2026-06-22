namespace LogisticsDeliveryManager.Domain.Services.Drivers;

public interface IDriverJourneyPolicy
{
    void ValidateJourney(TimeSpan plannedJourneyDuration, TimeSpan dailyDrivenDuration);
}
