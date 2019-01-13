using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .ToTable("Product");

            builder.Property(pr => pr.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .HasIndex(u => u.Name)
                .IsUnique();

        }
    }
}
