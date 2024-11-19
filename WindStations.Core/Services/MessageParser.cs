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
        const uint IntegerDivisionFactor = 1000000;
        const uint AnemometerFactorDivider = 100;
        const uint CupsArmDiameterMillimeters = 115;
        const float MpsToKnots = 1.9438F;
        const float SpeedConversionFactor = (CupsArmDiameterMillimeters * float.Pi) / (IntegerDivisionFactor * AnemometerFactorDivider);

        var minSpeed = reader.ReadInt32() * SpeedConversionFactor * MpsToKnots;
        var avgSpeed = reader.ReadInt32() * SpeedConversionFactor * MpsToKnots;
        var maxSpeed = reader.ReadInt32() * SpeedConversionFactor * MpsToKnots;

        float xComponentSumRad = 0;
        float yComponentSumRad = 0;

        for (var i = 0; i < 5; i++)
        {
            var directionSampleRad = reader.ReadUInt16() * float.Pi / 180;
            xComponentSumRad += (float)Math.Cos(directionSampleRad);
            yComponentSumRad += (float)Math.Sin(directionSampleRad);
        }

        const int OffsetDeg = 48;
        var avgDirectionDeg = (float)Math.Atan2(yComponentSumRad, xComponentSumRad) * 180 / float.Pi - OffsetDeg;

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
