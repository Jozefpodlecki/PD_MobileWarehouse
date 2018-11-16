using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class AttributeEntityConfiguration : IEntityTypeConfiguration<Attribute>
    {
        public void Configure(EntityTypeBuilder<Attribute> builder)
        {
            builder.Property(pr => pr.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .HasIndex(u => u.Name)
                .IsUnique();

            builder.Property(pr => pr.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(pr => pr.IsDeleted)
                .HasDefaultValueSql("(0)");

            builder
                .HasOne(at => at.LastModificationBy)
                .WithMany(us => us.Attributes)
                .HasForeignKey(at => at.LastModificationById)
                .HasConstraintName("FK_Attribute_User")
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .Property(pr => pr.LastModification)
                .HasDefaultValueSql("GETDATE()");

            builder
                .Property(pr => pr.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            builder
                .Property(pr => pr.Type)
                .HasDefaultValueSql("0");

            builder
                .Property(pr => pr.Order)
                .HasDefaultValueSql("0");
        }
    }
}
