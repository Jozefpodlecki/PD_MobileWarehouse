using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class CounterpartyEntityConfiguration : IEntityTypeConfiguration<Counterparty>
    {
        public void Configure(EntityTypeBuilder<Counterparty> builder)
        {
            builder
                .Property(i => i.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(i => i.PostalCode)
                .HasMaxLength(10);

            builder
                .Property(i => i.Street)
                .HasMaxLength(50);

            builder
                .Property(i => i.NIP)
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(i => i.PhoneNumber)
                .HasMaxLength(20);
        }
    }
}
