namespace WindStations.Core.DTOs;
public class RecordDTO(DateTime timestamp, float value)
{
    public DateTime Timestamp { get; } = timestamp;
    public float Value { get; } = value;
}
