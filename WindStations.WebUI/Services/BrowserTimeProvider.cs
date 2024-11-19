namespace WindStations.WebUI.Services;
public class BrowserTimeProvider : TimeProvider
{
    private TimeZoneInfo? _browserLocalTimeZone;

    public event EventHandler? LocalTimeZoneChanged;

    public override TimeZoneInfo LocalTimeZone => _browserLocalTimeZone ?? TimeZoneInfo.Utc;

    internal bool IsLocalTimeZoneSet => _browserLocalTimeZone != null;

    public void SetBrowserTimeZone(string timeZone)
    {
        if (!TimeZoneInfo.TryFindSystemTimeZoneById(timeZone, out var timeZoneInfo))
        {
            timeZoneInfo = null;
        }

        if (timeZoneInfo != LocalTimeZone)
        {
            _browserLocalTimeZone = timeZoneInfo;
            LocalTimeZoneChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public DateTime ToLocalDateTime(DateTime utcTime)
    {
        return TimeZoneInfo.ConvertTimeFromUtc(utcTime, LocalTimeZone);
    }
}
