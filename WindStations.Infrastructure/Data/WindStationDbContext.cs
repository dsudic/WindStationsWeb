using Microsoft.EntityFrameworkCore;
using WindStations.Core.Models;
using WindStations.Infrastructure.Configurations;

namespace WindStations.Infrastructure.Data;
public class WindStationDbContext(DbContextOptions<WindStationDbContext> options) : DbContext(options)
{
    public DbSet<Anemometer> Anemometer { get; set; }
    public DbSet<Vane> Vane { get; set; }
    public DbSet<Core.Models.Environment> Environment { get; set; }
    public DbSet<Battery> Battery { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new VaneConfiguration());
    }
}
