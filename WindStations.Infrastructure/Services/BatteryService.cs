using Microsoft.EntityFrameworkCore;
using WindStations.Core.Interfaces;
using WindStations.Infrastructure.Data;

namespace WindStations.Infrastructure.Services;
public class BatteryService(WindStationDbContext dbContext) : IBatteryService
{
    public async Task<uint> GetBatteryStatusAsync()
    {
        const float BatteryVoltageMax = 4.2F;
        var batteryVoltage = await dbContext.Battery.OrderByDescending(b => b.TimeStamp).Select(b => b.Voltage).FirstAsync();

        return (uint)Math.Round(batteryVoltage / BatteryVoltageMax * 100);
    }
}
