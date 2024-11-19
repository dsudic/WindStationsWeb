using System.Drawing;

namespace WindStations.WebUI.Components;
public class GradientStop(int value, Color color)
{
    public int Value { get; } = value;
    public Color Color { get; } = color;
}
