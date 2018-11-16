using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class InvoiceEntityConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder
               .Property(i => i.Total)
               .HasColumnType("Money")
               .IsRequired();

            builder
               .Property(i => i.VAT)
               .HasColumnType("Money")
               .IsRequired();

            builder
               .Property(i => i.DocumentId)
               .HasMaxLength(50)
               .IsRequired();

            builder
               .Property(i => i.CanEdit)
               .IsRequired();

            builder
               .Property(i => i.IssueDate)
               .IsRequired();

            builder
               .Property(i => i.CompletionDate)
               .IsRequired();
        }
    }
}
