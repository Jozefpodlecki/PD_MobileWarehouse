using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(pr => pr.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder
                .HasIndex(u => u.Name)
                .IsUnique();

            builder.Property(pr => pr.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(pr => pr.IsDeleted)
                .HasDefaultValueSql("(0)");

            builder
                .Property(pr => pr.LastModification)
                .HasDefaultValueSql("GETDATE()");

            builder
                .Property(pr => pr.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder
                .HasOne(at => at.LastModificationBy)
                .WithMany(us => us.Products)
                .HasForeignKey(at => at.LastModificationById)
                .HasConstraintName("FK_Product_User")
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
