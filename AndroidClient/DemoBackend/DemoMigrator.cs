using System;
using System.Collections.Generic;
using System.Linq;
using Client.Repository;
using Common.Managers;
using Common.Mappers;
using Common.Repository;
using Common.Repository.Interfaces;
using Data_Access_Layer;
using SQLite;
using Attribute = Data_Access_Layer.Attribute;

namespace Client.DemoBackend
{
    public class DemoMigrator
    {
        private INameRepository<Data_Access_Layer.Product> _productRepository;
        public INameRepository<Data_Access_Layer.Product> ProductRepository => _productRepository = _productRepository ?? new NameRepository<Data_Access_Layer.Product>(_sqliteConnection);

        private INameRepository<Data_Access_Layer.Attribute> _attributeRepository;
        public INameRepository<Data_Access_Layer.Attribute> AttributeRepository => _attributeRepository = _attributeRepository ?? new NameRepository<Data_Access_Layer.Attribute>(_sqliteConnection);

        private IRepository<Data_Access_Layer.ProductAttribute> _productAttributeRepository;
        public IRepository<Data_Access_Layer.ProductAttribute> ProductAttributeRepository => _productAttributeRepository = _productAttributeRepository ?? new Repository<Data_Access_Layer.ProductAttribute>(_sqliteConnection);

        private IClaimsRepository _claimsRepository;
        public IClaimsRepository ClaimsRepository => _claimsRepository = _claimsRepository ?? new ClaimsRepository();

        private IUserRepository _userRepository;
        public IUserRepository UserRepository => _userRepository = _userRepository ?? new UserRepository(_sqliteConnection);

        private INameRepository<Data_Access_Layer.Role> _roleRepository;
        public INameRepository<Data_Access_Layer.Role> RoleRepository => _roleRepository = _roleRepository ?? new NameRepository<Data_Access_Layer.Role>(_sqliteConnection);

        private IRepository<UserRole> _userRoleRepository;
        public IRepository<UserRole> UserRoleRepository => _userRoleRepository = _userRoleRepository ?? new Repository<Data_Access_Layer.UserRole>(_sqliteConnection);

        private IRoleClaimRepository _roleClaimRepository;
        public IRoleClaimRepository RoleClaimRepository => _roleClaimRepository = _roleClaimRepository ?? new RoleClaimRepository(_sqliteConnection);

        private IUserClaimRepository _userClaimRepository;
        public IUserClaimRepository UserClaimRepository => _userClaimRepository = _userClaimRepository ?? new UserClaimRepository(_sqliteConnection);

        private INameRepository<Data_Access_Layer.Counterparty> _counterpartyRepository;
        public INameRepository<Data_Access_Layer.Counterparty> CounterpartyRepository => _counterpartyRepository = _counterpartyRepository ?? new NameRepository<Data_Access_Layer.Counterparty>(_sqliteConnection);

        private INameRepository<Data_Access_Layer.City> _cityRepository;
        public INameRepository<Data_Access_Layer.City> CityRepository => _cityRepository = _cityRepository ?? new NameRepository<Data_Access_Layer.City>(_sqliteConnection);

        private IInvoiceRepository _invoiceRepository;
        public IInvoiceRepository InvoiceRepository => _invoiceRepository = _invoiceRepository ?? new InvoiceRepository(_sqliteConnection);

        private IEntryRepository _entryRepository;
        public IEntryRepository EntryRepository => _entryRepository = _entryRepository ?? new EntryRepository(_sqliteConnection);

        private IRepository<Data_Access_Layer.GoodsDispatchedNote> _goodsDispatchedNoteRepository;
        public IRepository<Data_Access_Layer.GoodsDispatchedNote> GoodsDispatchedNoteRepository => _goodsDispatchedNoteRepository = _goodsDispatchedNoteRepository ?? new Repository<Data_Access_Layer.GoodsDispatchedNote>(_sqliteConnection);

        private IRepository<Data_Access_Layer.GoodsReceivedNote> _goodsReceivedNoteRepository;
        public IRepository<Data_Access_Layer.GoodsReceivedNote> GoodsReceivedNoteRepository => _goodsReceivedNoteRepository = _goodsReceivedNoteRepository ?? new Repository<Data_Access_Layer.GoodsReceivedNote>(_sqliteConnection);

        private IProductDetailsRepository _productDetailsRepository;
        public IProductDetailsRepository ProductDetailsRepository => _productDetailsRepository = _productDetailsRepository ?? new ProductDetailsRepository(_sqliteConnection);

        private ILocationRepository _locationRepository;
        public ILocationRepository LocationRepository => _locationRepository = _locationRepository ?? new LocationRepository(_sqliteConnection);

        private readonly ISQLiteConnection _sqliteConnection;
        private readonly Mapper _mapper;
        private readonly IPasswordManager _passwordManager;

        public DemoMigrator(
            SQLiteConnectionManager sqliteConnectionManager,
            IPasswordManager passwordManager)
        {
            _passwordManager = passwordManager;
            _sqliteConnection = sqliteConnectionManager.Connection;
        }

        public void DropTables()
        {
            _sqliteConnection.DropTable<Data_Access_Layer.GoodsDispatchedNote>();
            _sqliteConnection.DropTable<Data_Access_Layer.GoodsReceivedNote>();
            _sqliteConnection.DropTable<Data_Access_Layer.Entry>();
            _sqliteConnection.DropTable<Data_Access_Layer.Invoice>();
            _sqliteConnection.DropTable<Data_Access_Layer.ProductAttribute>();
            _sqliteConnection.DropTable<Data_Access_Layer.ProductDetail>();
            _sqliteConnection.DropTable<Data_Access_Layer.Attribute>();
            _sqliteConnection.DropTable<Data_Access_Layer.Product>();
            _sqliteConnection.DropTable<Data_Access_Layer.Location>();
            _sqliteConnection.DropTable<Data_Access_Layer.Counterparty>();
            _sqliteConnection.DropTable<Data_Access_Layer.City>();
            _sqliteConnection.DropTable<Data_Access_Layer.UserRole>();
            _sqliteConnection.DropTable<Data_Access_Layer.RoleClaim>();
            _sqliteConnection.DropTable<Data_Access_Layer.Role>();
            _sqliteConnection.DropTable<Data_Access_Layer.UserClaim>();
            _sqliteConnection.DropTable<Data_Access_Layer.UserLogin>();
            _sqliteConnection.DropTable<Data_Access_Layer.UserToken>();
            _sqliteConnection.DropTable<Data_Access_Layer.User>();
        }

        public void CreateTables()
        {
            _sqliteConnection.CreateTable<Data_Access_Layer.User>();
            _sqliteConnection.CreateTable<Data_Access_Layer.Attribute>();
            _sqliteConnection.CreateTable<Data_Access_Layer.City>();
            _sqliteConnection.CreateTable<Data_Access_Layer.Counterparty>();
            _sqliteConnection.CreateTable<Data_Access_Layer.Entry>();
            _sqliteConnection.CreateTable<Data_Access_Layer.GoodsDispatchedNote>();
            _sqliteConnection.CreateTable<Data_Access_Layer.GoodsReceivedNote>();
            _sqliteConnection.CreateTable<Data_Access_Layer.Invoice>();
            _sqliteConnection.CreateTable<Data_Access_Layer.Location>();
            _sqliteConnection.CreateTable<Data_Access_Layer.Product>();
            _sqliteConnection.CreateTable<Data_Access_Layer.ProductAttribute>();
            _sqliteConnection.CreateTable<Data_Access_Layer.ProductDetail>();
            _sqliteConnection.CreateTable<Data_Access_Layer.Role>();
            _sqliteConnection.CreateTable<Data_Access_Layer.RoleClaim>();
            _sqliteConnection.CreateTable<Data_Access_Layer.UserClaim>();
            _sqliteConnection.CreateTable<Data_Access_Layer.UserLogin>();
            _sqliteConnection.CreateTable<Data_Access_Layer.UserRole>();
            _sqliteConnection.CreateTable<Data_Access_Layer.UserToken>();
        }

        public void Migrate()
        {
            DropTables();
            CreateTables();

            if (UserRepository.Any())
            {
                return;
            }

            var role = new Role
            {
                Name = "Administrator"
            };

            RoleRepository.Add(role);

            var roleClaims = ClaimsRepository
                .GetClaims()
                .Select(cl => new RoleClaim
                {
                    RoleId = role.Id,
                    ClaimType = cl.Type,
                    ClaimValue = cl.Value
                });

            RoleClaimRepository.AddRange(roleClaims);

            var user = new User
            {
                UserName = "admin1",
                FirstName = "Jan",
                LastName = "Kocur",
                Email = "admin@test.pl",
                PasswordHash = _passwordManager.GetHash("123")
            };

            UserRepository.Add(user);

            var userRole = new UserRole
            {
                RoleId = role.Id,
                UserId = user.Id
            };

            UserRoleRepository.Add(userRole);

            var city = new City
            {
                Name = "Olsztyn"
            };

            CityRepository.Add(city);

            var counterparty = new Counterparty
            {
                Name = "Firma sprzedajaca buty",
                NIP = "1122334455",
                PhoneNumber = "1122334455666",
                Street = "Lubelska 432",
                PostalCode = "11-341",
                CityId = city.Id
            };

            CounterpartyRepository.Add(counterparty);

            var location = new Location
            {
                Name = "Półka X"
            };

            LocationRepository.Add(location);

            var location1 = new Location
            {
                Name = "Półka Y"
            };

            LocationRepository.Add(location1);

            var invoice = new Invoice
            {
                DocumentId = "DOK-" + DateTime.Now.ToString(),
                CounterpartyId = counterparty.Id,
                CityId = city.Id,
                InvoiceType = Common.InvoiceType.Purchase,
                PaymentMethod = Common.PaymentMethod.Card,
                IssueDate = DateTime.Now,
                CompletionDate = DateTime.Now
            };

            InvoiceRepository.Add(invoice);

            var entries = new List<Entry>()
            {
                new Entry
                {
                    Name = "Buty 1",
                    InvoiceId = invoice.Id,
                    Price = 22.22m,
                    Count = 100,
                    VAT = 0.01m
                }
            };

            EntryRepository.AddRange(entries);

            invoice.Total = 100 * 22.22m;
            invoice.VAT = invoice.Total * 0.01m;
            InvoiceRepository.Update(invoice);

            var attribute = new Attribute
            {
                Name = "Firma"
            };

            AttributeRepository.Add(attribute);

            var attribute1 = new Attribute
            {
                Name = "Rodzaj"
            };

            AttributeRepository.Add(attribute1);

            var product = new Product
            {
                Name = "Buty1",
            };

            ProductRepository.Add(product);

            var productAttributes = new List<ProductAttribute>()
            {
                new ProductAttribute
                {
                    ProductId = product.Id,
                    AttributeId = attribute.Id,
                    Value = "Xinmex"
                },
                new ProductAttribute
                {
                    ProductId = product.Id,
                    AttributeId = attribute1.Id,
                    Value = "Obuwie skórzane"
                }
            };

            ProductAttributeRepository.AddRange(productAttributes);

            var productDetails = new List<ProductDetail>()
            {
                new ProductDetail
                {
                    LocationId = location.Id,
                    ProductId = product.Id,
                    Count = 100
                },
                new ProductDetail
                {
                    LocationId = location1.Id,
                    ProductId = product.Id,
                    Count = 50
                }
            };

            ProductDetailsRepository.AddRange(productDetails);
        }
    }
}