using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class NoteEntityConfiguration
        : IEntityTypeConfiguration<GoodsReceivedNote>,
        IEntityTypeConfiguration<GoodsDispatchedNote>
    {
        public void Configure(EntityTypeBuilder<GoodsReceivedNote> builder)
        {
            builder
                .ToTable("GoodsReceivedNote");

            builder
                .Property(pr => pr.DocumentId)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasIndex(u => u.DocumentId)
                .IsUnique();

            builder
                .HasKey(pr => pr.InvoiceId);

            builder
                .HasOne(pr => pr.Invoice)
                .WithOne()
                .HasForeignKey<GoodsReceivedNote>(pr => pr.InvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .Property(i => i.IssueDate)
               .HasColumnType("datetime2(0)")
               .IsRequired();

            builder
               .Property(i => i.ReceiveDate)
               .HasColumnType("datetime2(0)")
               .IsRequired();
        }

        public void Configure(EntityTypeBuilder<GoodsDispatchedNote> builder)
        {
            builder
                .ToTable("GoodsDispatchedNote");

            builder
                .Property(pr => pr.DocumentId)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .HasIndex(u => u.DocumentId)
                .IsUnique();

            builder
                .HasKey(pr => pr.InvoiceId);

            builder
                .HasOne(pr => pr.Invoice)
                .WithOne()
                .HasForeignKey<GoodsDispatchedNote>(pr => pr.InvoiceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .Property(i => i.IssueDate)
               .HasColumnType("datetime2(0)")
               .IsRequired();

            builder
               .Property(i => i.DispatchDate)
               .HasColumnType("datetime2(0)")
               .IsRequired();
        }
    }
}
