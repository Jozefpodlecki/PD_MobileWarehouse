using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder
                .ToTable("UserRole");

            builder
                .HasKey(r => new { r.UserId, r.RoleId });

            builder
                .HasOne(e => e.Role)
                .WithMany(pr => pr.UserRoles)
                .HasForeignKey(e => e.RoleId)
                .IsRequired();

            builder
                .HasOne(e => e.User)
                .WithMany(pr => pr.UserRoles)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

        }
    }
}
