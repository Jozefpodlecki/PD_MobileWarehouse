using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data_Access_Layer.EntityTypeConfiguration
{
    public class UserTokenEntityConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder
                .ToTable("UserToken");

            builder
                .Property(pr => pr.LoginProvider)
                .HasMaxLength(150)
                .IsRequired();

            builder
                .Property(pr => pr.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder
                .Property(pr => pr.Value)
                .HasMaxLength(1000)
                .IsRequired();
        }
    }
}
