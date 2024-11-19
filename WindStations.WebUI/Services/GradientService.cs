using System.Drawing;
using WindStations.WebUI.Components;

namespace WindStations.WebUI.Services;
public class GradientService
{
    public List<GradientStop> WindGradientStops { get; } = [
        new GradientStop(value: 0, color: Color.FromArgb(240, 244, 245)),
        new GradientStop(value: 7, color: Color.FromArgb(88, 208, 223)),
        new GradientStop(value: 14, color: Color.FromArgb(41, 193, 58)),
        new GradientStop(value: 20, color: Color.FromArgb(225, 226, 31)),
        new GradientStop(value: 27, color: Color.FromArgb(214, 131, 24)),
        new GradientStop(value: 34, color: Color.FromArgb(212, 32, 32)),
        new GradientStop(value: 42, color: Color.FromArgb(119, 14, 206)),
        new GradientStop(value: 50, color: Color.FromArgb(49, 18, 72))
        ];

    public string GetGradientColor(double value)
    {
        var lowerGradientStop = WindGradientStops.LastOrDefault(gradientStop => gradientStop.Value <= value) ?? WindGradientStops.First();
        var upperGradientStop = WindGradientStops.FirstOrDefault(gradientStop => gradientStop.Value >= value) ?? WindGradientStops.Last();

        if (lowerGradientStop == upperGradientStop)
        {
            return ColorTranslator.ToHtml(lowerGradientStop.Color);
        }

        var fraction = (value - lowerGradientStop.Value) / (upperGradientStop.Value - lowerGradientStop.Value);

        return ColorTranslator.ToHtml(InterpolateColor(lowerGradientStop.Color, upperGradientStop.Color, fraction));
    }

    public string GetGradientStops(double maxValue)
    {
        var gradientStops = new System.Text.StringBuilder();
        const double GradientOpacity = 0.4;

        var includedGradientStops = WindGradientStops.Where(gradientStop => gradientStop.Value < maxValue).ToList();

        foreach (var gradientStop in includedGradientStops)
        {
            gradientStops.AppendLine($"<stop offset=\"{gradientStop.Value / maxValue * 100}%\" " +
                $"stop-color=\"{ColorTranslator.ToHtml(gradientStop.Color)}\" " +
                $"stop-opacity=\"{GradientOpacity}\"/>");
        }

        gradientStops.AppendLine($"<stop offset=\"100%\" " +
            $"stop-color=\"{GetGradientColor(maxValue)}\" " +
            $"stop-opacity=\"{GradientOpacity}\"/>");

        return gradientStops.ToString();
    }

    private static Color InterpolateColor(Color firstColor, Color secondColor, double fraction)
    {
        var r = (byte)(firstColor.R + (secondColor.R - firstColor.R) * fraction);
        var g = (byte)(firstColor.G + (secondColor.G - firstColor.G) * fraction);
        var b = (byte)(firstColor.B + (secondColor.B - firstColor.B) * fraction);

        return Color.FromArgb(r, g, b);
    }
}
