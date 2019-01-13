using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class RoleClaimEntityConfiguration : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            builder
                .ToTable("RoleClaim");

            builder
                .HasOne(e => e.Role)
                .WithMany(pr => pr.RoleClaims)
                .HasForeignKey(e => e.RoleId)
                .IsRequired();

            builder
                .Property(pr => pr.ClaimType)
                .HasMaxLength(100);

            builder
                .Property(pr => pr.ClaimValue)
                .HasMaxLength(100);

            builder
                .HasAlternateKey(pr => new { pr.Id, pr.ClaimType, pr.ClaimValue });
        }
    }
}
