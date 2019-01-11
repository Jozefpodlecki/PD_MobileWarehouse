using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class AttributeEntityConfiguration : IEntityTypeConfiguration<Attribute>
    {
        public void Configure(EntityTypeBuilder<Attribute> builder)
        {
            builder
                .ToTable("Attribute");

            builder.Property(pr => pr.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasIndex(u => u.Name)
                .IsUnique();

            builder
                .Property(pr => pr.Order)
                .HasDefaultValueSql("0");
        }
    }
}
