using Microsoft.EntityFrameworkCore;
using WindStations.Core.DTOs;
using WindStations.Core.Interfaces;
using WindStations.Infrastructure.Data;

namespace WindStations.Infrastructure.Services;
public class WindService(WindStationDbContext dbContext) : IWindService
{
    public async Task<List<WindDTO>> GetWindDataAsync()
    {
        return await dbContext.Wind
            .OrderByDescending(wind => wind.Timestamp)
            .Select(wind => new WindDTO(wind.Timestamp, wind.MinSpeed, wind.AvgSpeed, wind.MaxSpeed, wind.Direction))
            .Take(15)
            .ToListAsync();
    }

    public async Task<float> GetLatestDirectionAsync()
    {
        return await dbContext.Wind
            .OrderByDescending(wind => wind.Timestamp)
            .Select(wind => wind.Direction)
            .FirstOrDefaultAsync();
    }

    public async Task<float?> GetLatestAvgSpeedAsync()
    {
        return await dbContext.Wind
            .OrderByDescending(wind => wind.Timestamp)
            .Select(wind => (float?)wind.AvgSpeed)
            .FirstOrDefaultAsync();
    }
}
