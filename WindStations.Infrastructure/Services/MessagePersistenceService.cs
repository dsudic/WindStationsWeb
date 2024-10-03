using Microsoft.EntityFrameworkCore;
using WindStations.Core.Interfaces;
using WindStations.Core.Services;
using WindStations.Infrastructure.Data;
using WindStations.Core.DTOs;

namespace WindStations.Infrastructure.Services;
public class MessagePersistenceService(WindStationDbContext dbContext, DataUpdateService updateService) : IMessagePersistenceService
{
    public async Task SaveAsync(byte[] payload)
    {
        using (var reader = new BinaryReader(new MemoryStream(payload)))
        {
            var stationId = MessageParser.ParseStationId(reader);

            var anemometer = MessageParser.ParseAnemometer(reader);
            dbContext.Anemometer.Add(anemometer);

            var vane = MessageParser.ParseVane(reader);
            dbContext.Vane.Add(vane);

            await updateService.SendLatestWindData(new WindDTO(
                timestamp: anemometer.TimeStamp,
                minSpeed: anemometer.MinSpeed,
                avgSpeed: anemometer.AvgSpeed,
                maxSpeed: anemometer.MaxSpeed,
                direction: vane.AvgDirection));

            var environment = MessageParser.ParseEnvironment(reader);
            dbContext.Environment.Add(environment);

            await updateService.SendLatestEnvironmentData(new EnvironmentDTO(
                timestamp: environment.TimeStamp,
                temperature: environment.Temperature,
                humidity: environment.Humidity,
                pressure: environment.Pressure));

            var battery = MessageParser.ParseBattery(reader);
            dbContext.Battery.Add(battery);

            await updateService.SendLatestStatusData(battery.TimeStamp, battery.Voltage);
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task<DateTime> GetLatestTimestampAsync()
    {
        return await dbContext.Anemometer
            .OrderByDescending(a => a.TimeStamp)
            .Select(a => a.TimeStamp)
            .FirstAsync();
    }
}
