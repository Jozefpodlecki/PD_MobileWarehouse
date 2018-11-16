using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder
                .HasIndex(u => u.Email)
                .IsUnique();

            builder
                .Property(pr => pr.PasswordHash)
                .HasMaxLength(64);

            builder
                .Property(pr => pr.PhoneNumber)
                .HasMaxLength(64);

            builder
                .Property(pr => pr.FirstName)
                .HasMaxLength(64);

            builder
                .Property(pr => pr.LastName)
                .HasMaxLength(64);

            builder
               .Property(pr => pr.SecurityStamp)
               .HasMaxLength(255);

            builder
               .Property(pr => pr.ConcurrencyStamp)
               .HasMaxLength(64);

            builder
              .HasMany<UserRole>()
              .WithOne()
              .HasForeignKey(e => e.UserId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(pr => pr.UserClaims)
                .WithOne(pr => pr.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
