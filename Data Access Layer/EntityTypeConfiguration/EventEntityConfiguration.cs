using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class EventEntityConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.Property(pr => pr.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .HasIndex(u => u.Name)
                .IsUnique();

            builder.Property(pr => pr.IsEnabled)
                .HasDefaultValueSql("(0)");
        }
    }
}
