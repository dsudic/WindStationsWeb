using Microsoft.EntityFrameworkCore;
using WindStations.Core.Interfaces;
using WindStations.Infrastructure.Data;

namespace WindStations.Infrastructure.Services;
public class WindService(WindStationDbContext dbContext) : IWindService
{
    public List<(DateTime Timestamp, float MinSpeed, float AvgSpeed, float MaxSpeed, float Direction)> GetWindData()
    {
        var anemometer = dbContext.Anemometer
            .OrderByDescending(a => a.TimeStamp)
            .Select(a => new { a.TimeStamp, a.MinSpeed, a.AvgSpeed, a.MaxSpeed })
            .Take(15)
            .AsEnumerable()
            .Select(a => (a.TimeStamp, a.MinSpeed, a.AvgSpeed, a.MaxSpeed))
            .ToList();

        var vane = dbContext.Vane.Select(v => v.AvgDirection).ToList();

        return anemometer.Zip(vane, (wind, direction) => (wind.TimeStamp, wind.MinSpeed, wind.AvgSpeed, wind.MaxSpeed, direction)).ToList();
    }

    public async Task<float> GetLatestDirectionAsync()
    {
        return await dbContext.Vane.OrderByDescending(v => v.TimeStamp).Select(v => v.AvgDirection).FirstAsync();
    }

    public async Task<float> GetLatestAvgSpeedAsync()
    {
        return await dbContext.Anemometer.OrderByDescending(a => a.TimeStamp).Select(a => a.AvgSpeed).FirstAsync();
    }
}
