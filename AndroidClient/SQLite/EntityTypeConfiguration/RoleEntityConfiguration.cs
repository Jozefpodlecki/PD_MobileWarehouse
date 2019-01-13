using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");

            builder
                .HasIndex(u => u.Name)
                .IsUnique();

            builder
                .HasMany(pr => pr.UserRoles)
                .WithOne(pr => pr.Role)
                .HasForeignKey(e => e.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(pr => pr.RoleClaims)
                .WithOne(pr => pr.Role)
                .HasForeignKey(e => e.RoleId)
                .IsRequired();
        }
    }
}
