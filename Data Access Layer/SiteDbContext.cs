using Common.Services;
using Data_Access_Layer.EntityTypeConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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

        private int? _userId;

        public SiteDbContext(
            DbContextOptions options,
            IUserResolverService userResolverService) : base(options)
        {
            _userId = userResolverService.GetUserId();
        }

        protected SiteDbContext(IUserResolverService userResolverService)
        {
            _userId = userResolverService.GetUserId();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserLoginEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserTokenEntityConfiguration());
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

        public override int SaveChanges()
        {
            UpdateBaseProperties();
            return base.SaveChanges();
        }

        private void UpdateBaseProperties()
        {
            var entities = ChangeTracker
                .Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            var currentDate = DateTime.UtcNow;

            foreach (var entity in entities)
            {
                var baseEntity = entity.Entity as IBaseEntity;

                if (baseEntity == null)
                {
                    continue;
                }

                if (entity.State == EntityState.Added)
                {
                    baseEntity.CreatedById = _userId;
                    baseEntity.CreatedAt = currentDate;
                }

                baseEntity.LastModifiedAt = currentDate;
                baseEntity.LastModifiedById = _userId;
            }
        }
    }
}
