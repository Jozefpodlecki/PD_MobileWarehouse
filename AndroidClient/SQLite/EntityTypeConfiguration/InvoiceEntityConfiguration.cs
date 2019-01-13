using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class InvoiceEntityConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder
                .ToTable("Invoice");

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
                .HasIndex(u => u.DocumentId)
                .IsUnique();

            builder
               .Property(i => i.CanEdit)
               .IsRequired();

            builder
                .HasOne(pr => pr.GoodsReceivedNote)
                .WithOne(pr => pr.Invoice);

            builder
                .HasOne(pr => pr.GoodsDispatchedNote)
                .WithOne(pr => pr.Invoice);

            builder
                .HasOne(pr => pr.Counterparty)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(pr => pr.City)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .Property(i => i.IssueDate)
               .HasColumnType("datetime2(0)")
               .IsRequired();

            builder
               .Property(i => i.CompletionDate)
               .HasColumnType("datetime2(0)")
               .IsRequired();
        }
    }
}
