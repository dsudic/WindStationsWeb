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

            var wind = MessageParser.ParseWind(reader);
            dbContext.Wind.Add(wind);

            await updateService.SendLatestWindData(new WindDTO(
                timestamp: wind.Timestamp,
                minSpeed: wind.MinSpeed,
                avgSpeed: wind.AvgSpeed,
                maxSpeed: wind.MaxSpeed,
                direction: wind.Direction));

            var environment = MessageParser.ParseEnvironment(reader);
            dbContext.Environment.Add(environment);

            await updateService.SendLatestEnvironmentData(new EnvironmentDTO(
                timestamp: environment.Timestamp,
                temperature: environment.Temperature,
                humidity: environment.Humidity,
                pressure: environment.Pressure));

            var battery = MessageParser.ParseBattery(reader);
            dbContext.Battery.Add(battery);

            await updateService.SendLatestStatusData(battery.Timestamp, battery.Voltage);
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task<DateTime> GetLatestTimestampAsync()
    {
        return await dbContext.Wind
            .OrderByDescending(wind => wind.Timestamp)
            .Select(wind => wind.Timestamp)
            .FirstOrDefaultAsync();
    }
}
