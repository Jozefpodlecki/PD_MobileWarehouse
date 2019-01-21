using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class UserLoginEntityConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder
                .ToTable("UserLogin");

            builder
                .Property(pr => pr.LoginProvider)
                .HasMaxLength(150)
                .IsRequired();

            builder
                 .Property(pr => pr.ProviderKey)
                 .HasMaxLength(150)
                 .IsRequired();

            builder
                .Property(pr => pr.ProviderDisplayName)
                .HasMaxLength(150);
        }
    }
}
