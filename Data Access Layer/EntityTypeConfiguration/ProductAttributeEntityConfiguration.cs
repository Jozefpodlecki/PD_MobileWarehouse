using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class ProductAttributeEntityConfiguration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.HasKey(key => new { key.ProductId, key.AttributeId });

            builder.Property(pr => pr.Value)
                .HasMaxLength(100)
                .IsRequired(false);

            builder
                .HasOne(pa => pa.Attribute)
                .WithMany(at => at.ProductAttributes)
                .HasForeignKey(pa => pa.AttributeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
