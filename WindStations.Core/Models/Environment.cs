namespace WindStations.Core.Models;

public class Environment(float temperature, float humidity, float pressure) : BaseEntity
{
    public float Temperature { get; private set; } = temperature;
    public float Humidity { get; private set; } = humidity;
    public float Pressure { get; private set; } = pressure;
}
