using Microsoft.EntityFrameworkCore;
using WindStations.Core.Models;

namespace WindStations.Infrastructure.Data;
public class WindStationDbContext(DbContextOptions<WindStationDbContext> options) : DbContext(options)
{
    public DbSet<Wind> Wind { get; set; }
    public DbSet<Core.Models.Environment> Environment { get; set; }
    public DbSet<Battery> Battery { get; set; }
}
