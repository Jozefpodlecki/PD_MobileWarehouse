using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class EntryEntityConfiguration : IEntityTypeConfiguration<Entry>
    {
        public void Configure(EntityTypeBuilder<Entry> builder)
        {
            builder.Property(pr => pr.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder
               .Property(i => i.Price)
               .HasColumnType("Money")
               .IsRequired();

            builder
               .Property(i => i.VAT)
               .HasColumnType("Money")
               .IsRequired();

            builder
               .Property(i => i.Count)
               .IsRequired();
        }
    }
}
