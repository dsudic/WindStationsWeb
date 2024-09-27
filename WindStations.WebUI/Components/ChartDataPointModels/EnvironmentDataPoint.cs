namespace WindStations.WebUI.Components.ChartDataPointModels;

public class EnvironmentDataPoint(DateTime timestamp, float value)
{
    public DateTime Timestamp { get; } = timestamp;
    public float Value { get; } = value;
}
