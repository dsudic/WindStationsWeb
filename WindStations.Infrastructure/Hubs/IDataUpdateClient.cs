using WindStations.Core.DTOs;

namespace WindStations.Infrastructure.Hubs;
public interface IDataUpdateClient
{
    Task ReceiveLatestWindData(WindDTO windData);
    Task ReceiveLatestTemperatureData(RecordDTO temperatureData);
    Task ReceiveLatestHumidityData(RecordDTO humidityData);
    Task ReceiveLatestPressureData(RecordDTO pressureData);
    Task ReceiveLatestStatusData(DateTime timestamp, float batteryVoltage);
}
