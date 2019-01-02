using Data_Access_Layer.EntityTypeConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data_Access_Layer.Entity
{
    public class SiteDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<GoodsReceivedNote> GoodsReceivedNotes { get; set; }
        public DbSet<GoodsDispatchedNote> GoodsDispatchedNotes { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Counterparty> Counterparties { get; set; }
        public DbSet<Entry> Entries { get; set; }

        public SiteDbContext(DbContextOptions options) : base(options)
        {
        }

        protected SiteDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserLogin>().ToTable("UserLogin");
            modelBuilder.Entity<UserToken>().ToTable("UserToken");

            modelBuilder.ApplyConfiguration(new RoleClaimEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserClaimEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new LocationEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CityEntityConfiguration());
            modelBuilder.ApplyConfiguration((IEntityTypeConfiguration<GoodsReceivedNote>)new NoteEntityConfiguration());
            modelBuilder.ApplyConfiguration((IEntityTypeConfiguration<GoodsDispatchedNote>)new NoteEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductAttributeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new AttributeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ProductDetailsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EntryEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CounterpartyEntityConfiguration());
        }
    }
}
