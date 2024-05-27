using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WindStations.Core.Models;

namespace WindStations.Infrastructure.Configurations;
public class VaneConfiguration : IEntityTypeConfiguration<Vane>
{
    public void Configure(EntityTypeBuilder<Vane> builder)
    {
        _ = builder.Property(wind => wind.CompassDirection).HasMaxLength(3);
    }
}
