using Microsoft.EntityFrameworkCore;
using WindStations.Core.Interfaces;
using WindStations.Core.Services;
using WindStations.Infrastructure.Data;

namespace WindStations.Infrastructure.Services;
public class MessagePersistenceService(WindStationDbContext dbContext) : IMessagePersistenceService
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

            var environment = MessageParser.ParseEnvironment(reader);
            dbContext.Environment.Add(environment);

            var battery = MessageParser.ParseBattery(reader);
            dbContext.Battery.Add(battery);
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
