using WindStations.Core.Models;
using Environment = WindStations.Core.Models.Environment;

namespace WindStations.Core.Services;
public static class MessageParser
{
    public static string ParseStationId(BinaryReader reader)
    {
        return System.Text.Encoding.UTF8.GetString(reader.ReadBytes(15));
    }

    public static Wind ParseWind(BinaryReader reader)
    {
        const float SpeedConversionFactor = 115 * float.Pi / (1000000 * 100);

        var minSpeed = reader.ReadInt32() * SpeedConversionFactor;
        var avgSpeed = reader.ReadInt32() * SpeedConversionFactor;
        var maxSpeed = reader.ReadInt32() * SpeedConversionFactor;

        float xComponentSumRad = 0;
        float yComponentSumRad = 0;

        for (var i = 0; i < 5; i++)
        {
            var directionSampleRad = reader.ReadUInt16() * float.Pi / 180;
            xComponentSumRad += (float)Math.Cos(directionSampleRad);
            yComponentSumRad += (float)Math.Sin(directionSampleRad);
        }

        var avgDirectionDeg = (float)Math.Atan2(yComponentSumRad, xComponentSumRad) * 180 / float.Pi;

        // avoid negative angles
        avgDirectionDeg = (avgDirectionDeg < 0) ? avgDirectionDeg + 360 : avgDirectionDeg;

        return new Wind(minSpeed, avgSpeed, maxSpeed, avgDirectionDeg);
    }

    public static Environment ParseEnvironment(BinaryReader reader)
    {
        var temperature = reader.ReadInt32() / (float)100;
        var humidity = reader.ReadUInt32() / (float)1000;
        var pressure = reader.ReadUInt32() / (float)100;

        return new Environment(temperature, humidity, pressure);
    }

    public static Battery ParseBattery(BinaryReader reader)
    {
        var batteryVoltage = reader.ReadUInt16() / (float)100;

        return new Battery(batteryVoltage);
    }
}
