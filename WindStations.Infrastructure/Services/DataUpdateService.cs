using Microsoft.AspNetCore.SignalR;
using WindStations.Core.DTOs;
using WindStations.Infrastructure.Hubs;

namespace WindStations.Infrastructure.Services;
public class DataUpdateService(IHubContext<DataUpdatesHub, IDataUpdateClient> hubContext)
{
    public async Task SendLatestWindData(WindDTO windData)
    {
        await hubContext.Clients.All.ReceiveLatestWindData(windData);
    }

    public async Task SendLatestEnvironmentData(EnvironmentDTO environmentData)
    {
        await hubContext.Clients.All.ReceiveLatestTemperatureData(new RecordDTO(
            timestamp: environmentData.Timestamp,
            value: environmentData.Temperature));

        await hubContext.Clients.All.ReceiveLatestHumidityData(new RecordDTO(
            timestamp: environmentData.Timestamp,
            value: environmentData.Humidity));

        await hubContext.Clients.All.ReceiveLatestPressureData(new RecordDTO(
            timestamp: environmentData.Timestamp,
            value: environmentData.Pressure));
    }

    public async Task SendLatestStatusData(DateTime timestamp, float batteryVoltage)
    {
        await hubContext.Clients.All.ReceiveLatestStatusData(timestamp, batteryVoltage);
    }
}
