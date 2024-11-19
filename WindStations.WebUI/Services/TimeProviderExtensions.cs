using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace WindStations.WebUI.Services;

public static class TimeProviderExtensions
{
    public static DateTime ToLocalDateTime(this TimeProvider timeProvider, DateTime utcTime)
    {
        return TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeProvider.LocalTimeZone);
    }
}
