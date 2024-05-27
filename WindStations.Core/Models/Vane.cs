using WindStations.Core.Enumerations;

namespace WindStations.Core.Models;

public class Vane(float avgDirection) : BaseEntity
{
    public float AvgDirection { get; private set; } = avgDirection;
    public string CompassDirection { get; private set; } = GetCompassDirection(avgDirection);

    private static string GetCompassDirection(float degrees)
    {
        var index = (int)((degrees / 22.5) + 0.5) % 16;
        return ((CompassDirections)index).ToString();
    }
}
