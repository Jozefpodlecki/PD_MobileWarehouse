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

        private IGoodsDispatchedNoteRepository _goodsDispatchedNoteRepository;
        public IGoodsDispatchedNoteRepository GoodsDispatchedNoteRepository => _goodsDispatchedNoteRepository = _goodsDispatchedNoteRepository ?? new GoodsDispatchedNoteRepository(_sqliteConnection);

        private IGoodsReceivedNoteRepository _goodsReceivedNoteRepository;
        public IGoodsReceivedNoteRepository GoodsReceivedNoteRepository => _goodsReceivedNoteRepository = _goodsReceivedNoteRepository ?? new GoodsReceivedNoteRepository(_sqliteConnection);

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

            var currentDate = DateTime.Now;

            var roleClaims = ClaimsRepository
                .GetClaims();

            var role = new Role
            {
                Name = "Administrator"
            };

            RoleRepository.Add(role);

            var role1 = new Role
            {
                Name = "Pracownik"
            };

            RoleRepository.Add(role1);

            var role2 = new Role
            {
                Name = "Księgowy"
            };

            RoleRepository.Add(role2);

            var role3 = new Role
            {
                Name = "Kierownik"
            };

            RoleRepository.Add(role3);

            var adminRoleClaims = roleClaims
                .Select(cl => new RoleClaim
                {
                    RoleId = role.Id,
                    ClaimType = cl.Type,
                    ClaimValue = cl.Value
                });

            var kierownikRoleClaims = roleClaims
                .Skip(4)
                .Select(cl => new RoleClaim
                {
                    RoleId = role3.Id,
                    ClaimType = cl.Type,
                    ClaimValue = cl.Value
                });

            var ksiegowyRoleClaims = roleClaims
                .Skip(8)
                .Select(cl => new RoleClaim
                {
                    RoleId = role2.Id,
                    ClaimType = cl.Type,
                    ClaimValue = cl.Value
                });

            var pracownikRoleClaims = roleClaims
                .Skip(12)
                .Select(cl => new RoleClaim
                {
                    RoleId = role1.Id,
                    ClaimType = cl.Type,
                    ClaimValue = cl.Value
                });

            RoleClaimRepository.AddRange(adminRoleClaims);
            RoleClaimRepository.AddRange(kierownikRoleClaims);
            RoleClaimRepository.AddRange(ksiegowyRoleClaims);
            RoleClaimRepository.AddRange(pracownikRoleClaims);

            var user = new User
            {
                UserName = "admin1",
                FirstName = "Jan",
                LastName = "Kocur",
                Email = "admin@test.pl",
                PasswordHash = _passwordManager.GetHash("123")
            };

            UserRepository.Add(user);

            var user1 = new User
            {
                UserName = "ksiegowy1",
                FirstName = "Władysław",
                LastName = "Kocioł",
                Email = "ksiegowy@test.pl",
                PasswordHash = _passwordManager.GetHash("123")
            };

            UserRepository.Add(user1);

            var user2 = new User
            {
                UserName = "pracownik1",
                FirstName = "Michał",
                LastName = "Zbożny",
                Email = "pracownik@test.pl",
                PasswordHash = _passwordManager.GetHash("123")
            };

            UserRepository.Add(user2);

            var user3 = new User
            {
                UserName = "kierownik1",
                FirstName = "Heniek",
                LastName = "Luwer",
                Email = "kierownik@test.pl",
                PasswordHash = _passwordManager.GetHash("123")
            };

            UserRepository.Add(user3);

            var userRole = new UserRole
            {
                RoleId = role.Id,
                UserId = user.Id
            };

            var userRole1 = new UserRole
            {
                RoleId = role2.Id,
                UserId = user1.Id
            };

            var userRole2 = new UserRole
            {
                RoleId = role1.Id,
                UserId = user2.Id
            };

            var userRole3 = new UserRole
            {
                RoleId = role3.Id,
                UserId = user3.Id
            };

            UserRoleRepository.Add(userRole);
            UserRoleRepository.Add(userRole1);
            UserRoleRepository.Add(userRole2);
            UserRoleRepository.Add(userRole3);

            var city = new City
            {
                Name = "Olsztyn"
            };

            CityRepository.Add(city);

            var city2 = new City
            {
                Name = "Warszawa"
            };

            CityRepository.Add(city2);

            var city3 = new City
            {
                Name = "Barczewo"
            };

            CityRepository.Add(city3);

            var city4 = new City
            {
                Name = "Gdańsk"
            };

            CityRepository.Add(city4);

            var city5 = new City
            {
                Name = "Kraków"
            };

            CityRepository.Add(city5);

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

            var counterparty1 = new Counterparty
            {
                Name = "Firma sprzedajaca ubrania",
                NIP = "2126337425",
                PhoneNumber = "1122334455666",
                Street = "Lubelska 432",
                PostalCode = "11-341",
                CityId = city.Id
            };

            CounterpartyRepository.Add(counterparty1);

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

            var location2 = new Location
            {
                Name = "Półka Z"
            };

            LocationRepository.Add(location2);

            var location3 = new Location
            {
                Name = "Półka K"
            };

            LocationRepository.Add(location3);

            var invoice = new Invoice
            {
                DocumentId = string.Format("FAK/{0:yyyyMMddhhmmssfff}", currentDate),
                CounterpartyId = counterparty.Id,
                CityId = city.Id,
                InvoiceType = Common.InvoiceType.Purchase,
                PaymentMethod = Common.PaymentMethod.Card,
                IssueDate = currentDate,
                CompletionDate = currentDate
            };

            InvoiceRepository.Add(invoice);

            var entries = new List<Entry>()
            {
                new Entry
                {
                    Name = "Buty Halwin Meain XL",
                    InvoiceId = invoice.Id,
                    Price = 22.22m,
                    Count = 1000,
                    VAT = 0.01m
                }
            };

            EntryRepository.AddRange(entries);

            invoice.Total = entries[0].Count * entries[0].Price;
            invoice.VAT = invoice.Total * entries[0].VAT;
            InvoiceRepository.Update(invoice);

            currentDate = DateTime.Now;

            var invoice1 = new Invoice
            {
                DocumentId = string.Format("FAK/{0:yyyyMMddhhmmssfff}", currentDate),
                CounterpartyId = counterparty.Id,
                CityId = city.Id,
                InvoiceType = Common.InvoiceType.Sales,
                PaymentMethod = Common.PaymentMethod.Card,
                IssueDate = currentDate,
                CompletionDate = currentDate
            };

            InvoiceRepository.Add(invoice1);

            var entries1 = new List<Entry>()
            {
                new Entry
                {
                    Name = "Buty Halwin Meain XL",
                    InvoiceId = invoice1.Id,
                    Price = 22.22m,
                    Count = 100,
                    VAT = 0.01m
                }
            };

            EntryRepository.AddRange(entries1);
            invoice1.Total = entries1[0].Count * entries1[0].Price;
            invoice1.VAT = invoice1.Total * entries1[0].VAT;
            InvoiceRepository.Update(invoice1);

            currentDate = DateTime.Now;

            var invoice2 = new Invoice
            {
                DocumentId = string.Format("FAK/{0:yyyyMMddhhmmssfff}", currentDate),
                CounterpartyId = counterparty.Id,
                CityId = city.Id,
                InvoiceType = Common.InvoiceType.Purchase,
                PaymentMethod = Common.PaymentMethod.Card,
                IssueDate = currentDate,
                CompletionDate = currentDate
            };

            InvoiceRepository.Add(invoice2);

            var entries2 = new List<Entry>()
            {
                new Entry
                {
                    Name = "Czapka Xiaming",
                    InvoiceId = invoice2.Id,
                    Price = 70.22m,
                    Count = 400,
                    VAT = 0.10m
                }
            };

            EntryRepository.AddRange(entries2);
            invoice2.Total = entries2[0].Count * entries2[0].Price;
            invoice2.VAT = invoice2.Total * entries2[0].VAT;
            InvoiceRepository.Update(invoice2);

            currentDate = DateTime.Now;

            var invoice3 = new Invoice
            {
                DocumentId = string.Format("FAK/{0:yyyyMMddhhmmssfff}", currentDate),
                CounterpartyId = counterparty.Id,
                CityId = city.Id,
                InvoiceType = Common.InvoiceType.Purchase,
                PaymentMethod = Common.PaymentMethod.Card,
                IssueDate = currentDate,
                CompletionDate = currentDate
            };

            InvoiceRepository.Add(invoice3);

            var entries3 = new List<Entry>()
            {
                new Entry
                {
                    Name = "Spodnie Jeans Amba M",
                    InvoiceId = invoice3.Id,
                    Price = 51.74m,
                    Count = 300,
                    VAT = 0.12m
                }
            };

            EntryRepository.AddRange(entries3);
            invoice3.Total = entries3[0].Count * entries3[0].Price;
            invoice3.VAT = invoice3.Total * entries3[0].VAT;
            InvoiceRepository.Update(invoice3);

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
                Name = "Buty Halwin Meain XL",
            };

            var product1 = new Product
            {
                Name = "Spodnie Jeans Amba M",
            };

            var product2 = new Product
            {
                Name = "Koszula Meadow L",
            };

            var product3 = new Product
            {
                Name = "Czapka Xiaming",
            };

            ProductRepository.Add(product);
            ProductRepository.Add(product1);
            ProductRepository.Add(product2);
            ProductRepository.Add(product3);

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
                },
                new ProductAttribute
                {
                    ProductId = product1.Id,
                    AttributeId = attribute.Id,
                    Value = "Abibas"
                },
                new ProductAttribute
                {
                    ProductId = product1.Id,
                    AttributeId = attribute1.Id,
                    Value = "Spodnie"
                },
                new ProductAttribute
                {
                    ProductId = product2.Id,
                    AttributeId = attribute.Id,
                    Value = "Pomba"
                },
                new ProductAttribute
                {
                    ProductId = product2.Id,
                    AttributeId = attribute1.Id,
                    Value = "Koszula bawelniana"
                }
            };

            ProductAttributeRepository.AddRange(productAttributes);

            var productDetails = new List<ProductDetail>()
            {
                new ProductDetail
                {
                    LocationId = location.Id,
                    ProductId = product.Id,
                    Count = 300
                },
                new ProductDetail
                {
                    LocationId = location1.Id,
                    ProductId = product.Id,
                    Count = 150
                },
                new ProductDetail
                {
                    LocationId = location.Id,
                    ProductId = product1.Id,
                    Count = 200
                },
                new ProductDetail
                {
                    LocationId = location1.Id,
                    ProductId = product1.Id,
                    Count = 150
                },
                new ProductDetail
                {
                    LocationId = location.Id,
                    ProductId = product2.Id,
                    Count = 600
                },
                new ProductDetail
                {
                    LocationId = location1.Id,
                    ProductId = product2.Id,
                    Count = 350
                },
                new ProductDetail
                {
                    LocationId = location.Id,
                    ProductId = product3.Id,
                    Count = 400
                },
                new ProductDetail
                {
                    LocationId = location1.Id,
                    ProductId = product3.Id,
                    Count = 250
                }
            };

            ProductDetailsRepository.AddRange(productDetails);

            var goodsDispatchedNote = new GoodsDispatchedNote
            {
                DocumentId = string.Format("PZ/{0:yyyyMMddhhmmss}", currentDate),
                Remarks = "Testowy opis...",
                InvoiceId = invoice1.Id,
                IssueDate = currentDate,
                DispatchDate = currentDate
            };

            GoodsDispatchedNoteRepository.Add(goodsDispatchedNote);

            var goodsReceivedNote = new GoodsReceivedNote
            {
                DocumentId = string.Format("WZ/{0:yyyyMMddhhmmss}", currentDate),
                Remarks = "Testowy opis...",
                InvoiceId = invoice.Id,
                IssueDate = currentDate,
                ReceiveDate = currentDate
            };

            GoodsReceivedNoteRepository.Add(goodsReceivedNote);


        }
    }
}