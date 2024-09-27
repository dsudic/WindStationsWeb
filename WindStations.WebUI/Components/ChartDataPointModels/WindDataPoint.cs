﻿namespace WindStations.WebUI.Components.ChartDataPointModels;
public class WindDataPoint(DateTime timestamp, float minSpeed, float avgSpeed, float maxSpeed, float direction)
{
    public DateTime Timestamp { get; } = timestamp;
    public float MinSpeed { get; } = minSpeed;
    public float AvgSpeed { get; } = avgSpeed;
    public float MaxSpeed { get; } = maxSpeed;
    public float Direction { get; } = direction;
}
