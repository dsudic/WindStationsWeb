namespace WindStations.Core.DTOs;
public class EnvironmentDTO(DateTime timestamp, float temperature, float humidity, float pressure)
{
    public DateTime Timestamp { get; } = timestamp;
    public float Temperature { get; } = temperature;
    public float Humidity { get; } = humidity;
    public float Pressure { get; } = pressure;
}
