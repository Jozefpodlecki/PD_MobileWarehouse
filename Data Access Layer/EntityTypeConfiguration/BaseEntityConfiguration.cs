using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T>
        where T: class, IBaseEntity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder
                .Property(pr => pr.LastModifiedAt)
                .HasColumnType("datetime2(0)")
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder
               .Property(i => i.CreatedAt)
               .HasColumnType("datetime2(0)")
               .IsRequired()
               .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
