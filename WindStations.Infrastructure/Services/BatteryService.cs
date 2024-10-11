using Microsoft.EntityFrameworkCore;
using WindStations.Core.Interfaces;
using WindStations.Infrastructure.Data;

namespace WindStations.Infrastructure.Services;
public class BatteryService(WindStationDbContext dbContext) : IBatteryService
{
    public async Task<float?> GetBatteryVoltageAsync()
    {
        return await dbContext.Battery
            .OrderByDescending(b => b.Timestamp)
            .Select(b => (float?)b.Voltage)
            .FirstOrDefaultAsync();
    }
}
