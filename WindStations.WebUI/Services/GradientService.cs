using System.Drawing;
using WindStations.WebUI.Components;

namespace WindStations.WebUI.Services;

public class GradientService
{
    public List<GradientRange> WindGradients { get; } = [
        new GradientRange(min: 0, max: 6, color: KnownColor.White),
        new GradientRange(min: 6, max: 12, color: KnownColor.DeepSkyBlue),
        new GradientRange(min: 12, max: 18, color: KnownColor.LimeGreen),
        new GradientRange(min: 18, max: 27, color: KnownColor.DarkOrange),
        new GradientRange(min: 27, max: 34, color: KnownColor.Red),
        new GradientRange(min: 34, max: 40, color: KnownColor.DeepPink),
        new GradientRange(min: 40, max: int.MaxValue, color: KnownColor.DarkViolet)
        ];

    public KnownColor GetRangeColor(double value)
    {
        foreach (var windGradient in WindGradients)
        {
            if ((value < windGradient.Max) && (value >= windGradient.Min))
            {
                return windGradient.Color;
            }
        }

        return KnownColor.Gray;
    }

    public string GetGradientStops(double maxValue)
    {
        var stops = new System.Text.StringBuilder();
        const double GradientOpacity = 0.4;

        foreach (var windGradient in WindGradients)
        {
            if ((windGradient.Max <= maxValue) || ((maxValue <= windGradient.Max) && (maxValue >= windGradient.Min)))
            {
                var offset = (windGradient.Max / maxValue) * 100;
                stops.AppendLine($"<stop offset='{offset}%' style='stop-color:{windGradient.Color};stop-opacity: {GradientOpacity}'/>");
            }
        }

        return stops.ToString();
    }
}
