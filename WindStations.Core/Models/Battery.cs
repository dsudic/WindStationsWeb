namespace WindStations.Core.Models;

public class Battery(float voltage) : BaseEntity
{
    public float Voltage { get; private set; } = voltage;
}
