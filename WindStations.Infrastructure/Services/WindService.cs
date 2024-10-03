using Microsoft.EntityFrameworkCore;
using WindStations.Core.DTOs;
using WindStations.Core.Interfaces;
using WindStations.Infrastructure.Data;

namespace WindStations.Infrastructure.Services;
public class WindService(WindStationDbContext dbContext) : IWindService
{
    public List<WindDTO> GetWindData()
    {
        var anemometer = dbContext.Anemometer
            .OrderByDescending(a => a.TimeStamp)
            .Select(a => new { a.TimeStamp, a.MinSpeed, a.AvgSpeed, a.MaxSpeed })
            .Take(15)
            .AsEnumerable()
            .Select(a => (a.TimeStamp, a.MinSpeed, a.AvgSpeed, a.MaxSpeed))
            .ToList();

        var vane = dbContext.Vane.Select(v => v.AvgDirection).ToList();

        return anemometer.Zip(vane, (wind, direction) => new WindDTO(
            timestamp: wind.TimeStamp,
            minSpeed: wind.MinSpeed,
            avgSpeed: wind.AvgSpeed,
            maxSpeed: wind.MaxSpeed,
            direction: direction))
            .ToList();
    }

    public async Task<float> GetLatestDirectionAsync()
    {
        return await dbContext.Vane
            .OrderByDescending(v => v.TimeStamp)
            .Select(v => v.AvgDirection)
            .FirstAsync();
    }

    public async Task<float> GetLatestAvgSpeedAsync()
    {
        return await dbContext.Anemometer
            .OrderByDescending(a => a.TimeStamp)
            .Select(a => a.AvgSpeed)
            .FirstAsync();
    }
}
