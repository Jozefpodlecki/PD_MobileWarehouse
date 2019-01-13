using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class ProductAttributeEntityConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder
                .ToTable("ProductAttribute");

            builder.HasKey(key => new { key.ProductId, key.AttributeId });

            builder
                .Property(pr => pr.Value)
                .HasMaxLength(255)
                .IsRequired();

            builder
                .HasOne(pa => pa.Attribute)
                .WithMany(at => at.ProductAttributes)
                .HasForeignKey(pa => pa.AttributeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
