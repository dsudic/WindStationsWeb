using System.Drawing;

namespace WindStations.WebUI.Components;
public class GradientRange(int min, int max, KnownColor color)
{
    public int Min { get; } = min;
    public int Max { get; } = max;
    public KnownColor Color { get; } = color;
}
