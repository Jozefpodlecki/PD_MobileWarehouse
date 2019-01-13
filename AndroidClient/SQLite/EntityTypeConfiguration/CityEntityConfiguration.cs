using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class CityEntityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder
                .ToTable("City");

            builder
                .Property(pr => pr.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasIndex(u => u.Name)
                .IsUnique();
        }
    }
}
