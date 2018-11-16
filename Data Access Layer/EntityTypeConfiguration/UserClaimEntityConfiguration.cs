using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class UserClaimEntityConfiguration : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder
                .ToTable("UserClaim");

            builder
                .Property(pr => pr.ClaimType)
                .HasMaxLength(100);

            builder
                .Property(pr => pr.ClaimValue)
                .HasMaxLength(100);

            builder
                .HasAlternateKey(pr => new { pr.Id, pr.ClaimType, pr.ClaimValue });

            builder
                .HasOne(e => e.User)
                .WithMany(pr => pr.UserClaims)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
        }
    }
}
