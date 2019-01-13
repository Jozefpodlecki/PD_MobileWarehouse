using Data_Access_Layer;
using Data_Access_Layer.EntityTypeConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using Attribute = Data_Access_Layer.Attribute;

namespace Client.SQLite
{
    public class SQLiteDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
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

        //public DbSet<Models.Product> Products { get; set; }
        //public DbSet<Models.Attribute> Attributes { get; set; }
        //public DbSet<Models.ProductAttribute> ProductAttributes { get; set; }
        //public DbSet<Models.GoodsReceivedNote> GoodsReceivedNotes { get; set; }
        //public DbSet<Models.GoodsDispatchedNote> GoodsDispatchedNotes { get; set; }
        //public DbSet<Models.Invoice> Invoices { get; set; }
        //public DbSet<Models.City> Cities { get; set; }
        //public DbSet<Models.Counterparty> Counterparties { get; set; }
        //public DbSet<Models.Entry> Entries { get; set; }
        //public DbSet<Models.User> Users { get; set; }
        //public DbSet<Models.Role> Roles { get; set; }
        //public DbSet<Models.Claim> Claims { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var databaseName = "demoTestDatabase.db";
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseName);
            optionsBuilder.UseSqlite($"Filename={databasePath}");
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

            modelBuilder.ApplyConfiguration(new BaseEntityConfiguration<User>());
            modelBuilder.ApplyConfiguration(new BaseEntityConfiguration<Role>());
            modelBuilder.ApplyConfiguration(new BaseEntityConfiguration<City>());
            modelBuilder.ApplyConfiguration(new BaseEntityConfiguration<Counterparty>());
            modelBuilder.ApplyConfiguration(new BaseEntityConfiguration<Attribute>());
            modelBuilder.ApplyConfiguration(new BaseEntityConfiguration<Product>());
            modelBuilder.ApplyConfiguration(new BaseEntityConfiguration<GoodsDispatchedNote>());
            modelBuilder.ApplyConfiguration(new BaseEntityConfiguration<GoodsReceivedNote>());
            modelBuilder.ApplyConfiguration(new BaseEntityConfiguration<Invoice>());
            modelBuilder.ApplyConfiguration(new BaseEntityConfiguration<Location>());
        }
    }
}