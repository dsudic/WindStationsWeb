namespace WindStations.Core.Models;

public class Anemometer(float minSpeed, float avgSpeed, float maxSpeed) : BaseEntity
{
    public float MinSpeed { get; private set; } = minSpeed;
    public float AvgSpeed { get; private set; } = avgSpeed;
    public float MaxSpeed { get; private set; } = maxSpeed;
}
