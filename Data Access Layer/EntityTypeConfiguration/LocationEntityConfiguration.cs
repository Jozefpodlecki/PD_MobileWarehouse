using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class LocationEntityConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.Property(pr => pr.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .HasIndex(u => u.Name)
                .IsUnique();

        }
    }
}
