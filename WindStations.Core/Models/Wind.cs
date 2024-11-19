namespace WindStations.Core.Models;

public class Wind(float minSpeed, float avgSpeed, float maxSpeed, float direction) : BaseEntity
{
    public float MinSpeed { get; private set; } = minSpeed;
    public float AvgSpeed { get; private set; } = avgSpeed;
    public float MaxSpeed { get; private set; } = maxSpeed;
    public float Direction { get; private set; } = direction;
}
