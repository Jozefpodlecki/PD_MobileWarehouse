
using Client.DemoBackend;
using Client.Repository;
using Common;
using Common.IUnitOfWork;
using Common.Managers;
using Common.Mappers;
using Common.Repository;
using Common.Repository.Interfaces;
using Data_Access_Layer;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApiServer.Controllers.Attribute.ViewModel;
using WebApiServer.Controllers.Counterparty.ViewModel;
using WebApiServer.Controllers.Invoice.ViewModel;
using WebApiServer.Controllers.Location.ViewModel;
using WebApiServer.Controllers.Note.ViewModel;
using WebApiServer.Controllers.Product.ViewModel;
using WebApiServer.Controllers.Role.ViewModel;
using WebApiServer.Controllers.User.ViewModel;

namespace Client
{
    public class UnitOfWork : IUnitOfWork
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

        public UnitOfWork(
            SQLiteConnectionManager sqliteConnectionManager,
            Mapper mapper,
            IPasswordManager passwordManager
            )
        {
            _mapper = mapper;
            _passwordManager = passwordManager;
            _sqliteConnection = sqliteConnectionManager.Connection;
        }

        private Task RunTaskInTransaction(Action action)
        {
            return Task.Run(() =>
            {
                _sqliteConnection.BeginTransaction();
                try
                {
                    action.Invoke();
                    _sqliteConnection.Commit();
                }
                catch (Exception ex)
                {
                    _sqliteConnection.Rollback();
                }
            });
        }

        public async Task AddAttribute(AddAttribute model)
        {
            await RunTaskInTransaction(() => {
                var attribute = new Data_Access_Layer.Attribute
                {
                    Name = model.Name
                };
                _sqliteConnection.Insert(attribute);
            });
        }

        public Data_Access_Layer.City GetOrCreateCity(Common.DTO.City model)
        {
            Data_Access_Layer.City city = null;

            if (model.Id != 0)
            {
                city = _sqliteConnection
                    .Find<Data_Access_Layer.City>(model.Id);

                return city;
            }

            city = _sqliteConnection
                .Table<Data_Access_Layer.City>()
                .FirstOrDefault(ci => ci.Name == model.Name);

            if(city == null)
            {
                city = new Data_Access_Layer.City
                {
                    Name = model.Name
                };

                _sqliteConnection.Insert(city);
            }

            return city;
        }

        public async Task AddCounterparty(AddCounterparty model)
        {
            await RunTaskInTransaction(() => {

                var city = GetOrCreateCity(model.City);

                var counterparty = new Data_Access_Layer.Counterparty
                {
                    Name = model.Name,
                    CityId = city.Id,
                    Street = model.PostalCode,
                    PhoneNumber = model.PhoneNumber,
                    NIP = model.NIP
                };
                _sqliteConnection.Insert(counterparty);
            });
        }

        public async Task AddGoodsDispatchedNote(AddGoodsDispatchedNote model)
        {
            await RunTaskInTransaction(async () =>
            {
                var note = new Data_Access_Layer.GoodsDispatchedNote
                {
                    DocumentId = model.DocumentId,
                    IssueDate = model.IssueDate,
                    DispatchDate = model.DispatchDate,
                    InvoiceId = model.InvoiceId
                };

                await GoodsDispatchedNoteRepository.Add(note);

                var invoiceEntries = await EntryRepository.GetForInvoice(model.InvoiceId);

                foreach (var noteEntry in model.NoteEntry)
                {
                    var productEntity = await ProductRepository.Find(noteEntry.Name);

                    var entry = invoiceEntries
                        .FirstOrDefault(ie => ie.Name == noteEntry.Name);

                    var productDetails = ProductDetailsRepository
                        .GetForProduct(productEntity.Id);

                    var productDetail = productDetails
                        .FirstOrDefault(pd => pd.Location.Id == noteEntry.Location.Id);

                    productDetail.Count -= entry.Count;

                    if (productDetail.Count <= 0)
                    {
                        ProductDetailsRepository.Remove(productDetail);
                    }
                    else
                    {
                        ProductDetailsRepository.Update(productDetail);
                    }

                    if (productDetails.Count == 1)
                    {
                        ProductRepository.Remove(productEntity);
                    }
                }
            });
        }

        public async Task AddGoodsReceivedNote(AddGoodsReceivedNote model)
        {
            await RunTaskInTransaction(async () =>
            {
                var note = new Data_Access_Layer.GoodsReceivedNote
                {
                    DocumentId = model.DocumentId,
                    IssueDate = model.IssueDate,
                    ReceiveDate = model.ReceiveDate,
                    InvoiceId = model.InvoiceId
                };

                await GoodsReceivedNoteRepository.Add(note);

                var invoiceEntries = await EntryRepository.GetForInvoice(model.InvoiceId);

                foreach (var noteEntry in model.NoteEntry)
                {
                    var productEntity = await ProductRepository.Find(noteEntry.Name);

                    Data_Access_Layer.ProductDetail productDetail = null;

                    var entry = invoiceEntries
                        .FirstOrDefault(ie => ie.Name == noteEntry.Name);

                    if (productEntity == null)
                    {

                        var product = new Data_Access_Layer.Product
                        {
                            Name = entry.Name,
                            Price = entry.Price,
                            VAT = entry.VAT
                        };

                        await ProductRepository.Add(product);

                        productDetail = new Data_Access_Layer.ProductDetail
                        {
                            LocationId = noteEntry.Location.Id,
                            ProductId = product.Id,
                            Count = entry.Count,
                        };

                        await ProductDetailsRepository.Add(productDetail);
                        
                        continue;
                    }

                    var productDetails = ProductDetailsRepository
                        .GetForProduct(productEntity.Id)
                        .FirstOrDefault(pd => pd.LocationId == noteEntry.Location.Id);

                    productDetails.Count += entry.Count;

                    ProductDetailsRepository.Update(productDetails);
                    
                }
            });
        }

        public async Task AddLocation(AddLocation model)
        {
            var location = new Data_Access_Layer.Location()
            {
                Name = model.Name
            };
            await LocationRepository.Add(location);
        }

        public async Task AddRole(AddRole model)
        {
            await RunTaskInTransaction(async () =>
            {
                var entity = new Data_Access_Layer.Role()
                {
                    Name = model.Name
                };

                await RoleRepository.Add(entity);

                await RoleClaimRepository
                    .AddRange(model.Claims.Select(cl => new RoleClaim
                    {
                        Role = entity,
                        ClaimType = cl.Type,
                        ClaimValue = cl.Value
                    }));
            });
        }

        public async Task AddRoles(IEnumerable<AddRole> roles)
        {
            await RunTaskInTransaction(async () =>
            {
                foreach (var model in roles)
                {
                    var entity = new Data_Access_Layer.Role()
                    {
                        Name = model.Name
                    };

                    await RoleRepository.Add(entity);

                    await RoleClaimRepository
                        .AddRange(model.Claims.Select(cl => new RoleClaim
                        {
                            Role = entity,
                            ClaimType = cl.Type,
                            ClaimValue = cl.Value
                        }));
                }
            });
        }

        public async Task AddUser(AddUser model)
        {
            await RunTaskInTransaction(async () =>
            {
            });
        }

        public async Task AddUsers(IEnumerable<AddUser> users)
        {
            await RunTaskInTransaction(async () =>
            {
                foreach (var model in users)
                {
                    var passwordHash = _passwordManager.GetHash(model.Password);

                    var entity = new Data_Access_Layer.User()
                    {
                        UserName = model.Username,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        PasswordHash = passwordHash
                    };

                    await UserRepository.Add(entity);

                    if (model.Role != null)
                    {
                        var role = await RoleRepository.Find(model.Role.Name);

                        if (role != null)
                        {

                            await UserRoleRepository.Add(
                                new UserRole
                                {
                                    UserId = entity.Id,
                                    RoleId = role.Id
                                });
                        }

                    }

                    if (model.Claims != null)
                    {
                        var claims = model.Claims.Select(cl => new UserClaim
                        {
                            UserId = entity.Id,
                            ClaimType = cl.Type,
                            ClaimValue = cl.Value
                        });

                        await UserClaimRepository.AddRange(claims);
                    }

                }
            });
        }

        public void BlockUser(Data_Access_Layer.User user)
        {
            user.UserStatus = UserStatus.BLOCKED;

            UserRepository.Update(user);
        }

        public async Task CreateInvoice(AddInvoice model)
        {
            await RunTaskInTransaction(async () => 
            {
                Data_Access_Layer.Invoice invoice = null;
                Data_Access_Layer.City city = null;

                if (model.City != null)
                {
                    city = GetOrCreateCity(model.City);
                }

                var entries = new List<Data_Access_Layer.Entry>();
                var totalVAT = 0m;
                var total = 0m;

                invoice = new Data_Access_Layer.Invoice
                {
                    CounterpartyId = model.Counterparty.Id,
                    CityId = model.City.Id,
                    CompletionDate = model.CompletionDate,
                    IssueDate = model.IssueDate,
                    DocumentId = model.DocumentId,
                    Total = total,
                    VAT = totalVAT,
                    InvoiceType = model.InvoiceType,
                    PaymentMethod = model.PaymentMethod,
                    CanEdit = model.CanEdit
                };

                if (city != null)
                {
                    invoice.CityId = city.Id;
                }

                await InvoiceRepository.Add(invoice);

                foreach (var product in model.Products)
                {
                    var entry = new Data_Access_Layer.Entry
                    {
                        Name = product.Name,
                        Price = product.Price,
                        Count = product.Count,
                        VAT = product.VAT,
                        InvoiceId = invoice.Id
                    };

                    var amount = entry.Count * entry.Price;
                    totalVAT += amount * entry.VAT;
                    total += amount;
                    entries.Add(entry);
                }

                await EntryRepository.AddRange(entries);

                invoice.Total = total;
                invoice.VAT = totalVAT;

                InvoiceRepository.Update(invoice);
            });
        }

        public async Task CreateInvoices(IEnumerable<AddInvoice> models)
        {
            await RunTaskInTransaction(async () =>
            {
                foreach (var model in models)
                {
                    Data_Access_Layer.Invoice invoice = null;
                    Data_Access_Layer.City city = null;

                    if (model.City != null)
                    {
                        city = GetOrCreateCity(model.City);
                    }

                    var entries = new List<Data_Access_Layer.Entry>();
                    var totalVAT = 0m;
                    var total = 0m;

                    if (model.IssueDate == DateTime.MinValue)
                    {
                        model.IssueDate = DateTime.Now;
                    }

                    if (model.CompletionDate == DateTime.MinValue)
                    {
                        model.CompletionDate = DateTime.Now;
                    }

                    invoice = new Data_Access_Layer.Invoice
                    {
                        CounterpartyId = model.Counterparty.Id,
                        CityId = model.City.Id,
                        CompletionDate = model.CompletionDate,
                        IssueDate = model.IssueDate,
                        DocumentId = model.DocumentId,
                        Total = total,
                        VAT = totalVAT,
                        PaymentMethod = model.PaymentMethod,
                        CanEdit = model.CanEdit
                    };

                    if (city != null)
                    {
                        invoice.CityId = city.Id;
                    }

                    await InvoiceRepository.Add(invoice);

                    foreach (var product in model.Products)
                    {
                        var entry = new Data_Access_Layer.Entry
                        {
                            Name = product.Name,
                            Price = product.Price,
                            Count = product.Count,
                            VAT = product.VAT,
                            InvoiceId = invoice.Id
                        };

                        var amount = entry.Count * entry.Price;
                        totalVAT += amount * entry.VAT;
                        total += amount;
                        entries.Add(entry);
                    }

                    await EntryRepository.AddRange(entries);

                    invoice.Total = total;
                    invoice.VAT = totalVAT;

                    InvoiceRepository.Update(invoice);
                }
            });
        }

        public async Task DeleteAttribute(int id)
        {
            var entity = await AttributeRepository.Get(id);
            AttributeRepository.Remove(entity);
        }

        public async Task DeleteLocation(int id)
        {
            var location = await LocationRepository.Get(id);
            LocationRepository.Remove(location);
        }

        public void DeleteRole(Data_Access_Layer.Role role)
        {
            RoleRepository.Remove(role);
        }

        public async Task DeleteUser(int id)
        {
            var user = await UserRepository.Get(id);

            user.UserStatus = UserStatus.DELETED;

            UserRepository.Update(user);
        }

        public async Task EditAttribute(EditAttribute model)
        {
            var entity = new Data_Access_Layer.Attribute
            {
                Id = model.Id,
                Name = model.Name
            };

            AttributeRepository.Update(entity);
        }

        public async Task EditLocation(EditLocation model)
        {
            var location = new Data_Access_Layer.Location()
            {
                Id = model.Id,
                Name = model.Name
            };

            LocationRepository.Update(location);
        }

        public async Task EditProduct(EditProduct model)
        {
            await RunTaskInTransaction(async () =>
            {
                var product = await ProductRepository.Get(model.Id);

                product.Image = model.Image;

                ProductRepository.Update(product);
                
                product.ProductAttributes.Clear();

                var attributesToAdd = model
                    .ProductAttributes
                    .Select(pa => pa.Attribute.Name)
                    .Except(
                        _attributeRepository
                        .Entities
                        .Select(at => at.Name))
                    .Select(at => new Data_Access_Layer.Attribute
                    {
                        Name = at
                    });

                await AttributeRepository.AddRange(attributesToAdd);

                var productAttributesToAdd = AttributeRepository
                    .Entities
                    .Join(model.ProductAttributes,
                        pv => pv.Name,
                        pv => pv.Attribute.Name,
                        (attr, prattr) => new Data_Access_Layer.ProductAttribute
                        {
                            ProductId = product.Id,
                            AttributeId = attr.Id,
                            Value = prattr.Value
                        });

                await ProductAttributeRepository.AddRange(productAttributesToAdd);
            });
        }

        public async Task EditRole(EditRole model)
        {
            await RunTaskInTransaction(async () =>
            {
                var entity = await RoleRepository.Get(model.Id);

                entity.Name = model.Name;

                entity.RoleClaims.Clear();

                await RoleClaimRepository
                    .AddRange(model.Claims.Select(cl => new RoleClaim
                    {
                        Role = entity,
                        ClaimType = cl.Type,
                        ClaimValue = cl.Value
                    }));

                RoleRepository.Update(entity);
            });
        }

        public async Task EditUser(EditUser model)
        {
            await RunTaskInTransaction(async () =>
            {
                var entity = await UserRepository.Get(model.Id);

                entity.UserName = model.Username;
                entity.Email = model.Email;
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Image = model.Avatar;

                if (!string.IsNullOrEmpty(model.Password))
                {
                    var passwordHash = _passwordManager.GetHash(model.Password);

                    if (!_passwordManager.Compare(entity, passwordHash))
                    {
                        entity.PasswordHash = passwordHash;
                    }
                }

                entity.UserRoles.Clear();

                if (model.Role != null)
                {
                    var role = await RoleRepository.Find(model.Role.Name);

                    await UserRoleRepository.Add(
                    new UserRole
                    {
                        User = entity,
                        Role = role
                    });
                }

                UserRepository.Update(entity);

                entity.UserClaims.Clear();

                if (model.Claims != null)
                {
                    var claims = model.Claims.Select(cl => new UserClaim
                    {
                        UserId = entity.Id,
                        ClaimType = cl.Type,
                        ClaimValue = cl.Value
                    });

                    await UserClaimRepository.AddRange(claims);
                }
            });
        }

        public bool ExistsCounterparty(ExistsCounterparty model)
        {
            return CounterpartyRepository
                .Entities
                .Any(co => co.NIP == model.NIP || co.Name == model.Name);
        }

        public async Task<List<Common.DTO.Attribute>> GetAttributes(FilterCriteria criteria)
        {
            return AttributeRepository
                .Get(criteria)
                .Select(_mapper.Map)
                .ToList();
        }

        public List<Common.DTO.City> GetCities(FilterCriteria criteria)
        {
            return CityRepository
                .Get(criteria)
                .Select(_mapper.Map)
                .ToList();
        }

        public List<Common.DTO.Claim> GetClaims()
        {
            return ClaimsRepository.GetClaims();
        }

        public List<Common.DTO.Counterparty> GetCounterparties(FilterCriteria criteria)
        {
            return CounterpartyRepository
                .Get(criteria)
                .Select(_mapper.Map)
                .ToList();
        }

        public List<Common.DTO.GoodsDispatchedNote> GetGoodsDispatchedNotes(FilterCriteria criteria)
        {
            return GoodsDispatchedNoteRepository
                .Get(criteria)
                .Select(_mapper.Map)
                .ToList();
        }

        public List<Common.DTO.GoodsReceivedNote> GetGoodsReceivedNotes(FilterCriteria criteria)
        {
            return GoodsReceivedNoteRepository
                .Get(criteria)
                .Select(_mapper.Map)
                .ToList();
        }

        public List<Common.DTO.Invoice> GetInvoices(InvoiceFilterCriteria criteria)
        {
            var result = InvoiceRepository.Entities;

            if (criteria.InvoiceType.HasValue)
            {
                result = result.Where(en => en.InvoiceType == criteria.InvoiceType.Value);
            }

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                result = result.Where(en => en.DocumentId.Contains(criteria.Name));
            }

            return result
                .Skip(criteria.Page * criteria.ItemsPerPage)
                .Take(criteria.ItemsPerPage)
                .Select(_mapper.Map)
                .ToList();
        }

        public List<KeyValue> GetInvoiceTypes()
        {
            return InvoiceRepository.GetInvoiceTypes();
        }

        public List<Common.DTO.Location> GetLocations(FilterCriteria criteria)
        {
            return LocationRepository
                .Get(criteria)
                .Select(_mapper.Map)
                .ToList();
        }

        public List<Common.DTO.Location> GetLocationsByProduct(string name)
        {
            return LocationRepository
                .GetLocationsByProduct(name)
                .Select(_mapper.Map)
                .ToList();
        }

        public List<KeyValue> GetPaymentMethods()
        {
            return InvoiceRepository.GetPaymentMethods();
        }

        public Common.DTO.Product GetProductByBarcode(string barcode)
        {
            return ProductRepository
                .Entities
                .Select(_mapper.Map)
                .FirstOrDefault(pr => pr.Barcode == barcode);
        }

        public List<Common.DTO.Product> GetProducts(FilterCriteria criteria)
        {
            return ProductRepository
                .Get(criteria)
                .Select(_mapper.Map)
                .ToList();
        }

        public async Task<Data_Access_Layer.Role> GetRole(int id)
        {
            return await RoleRepository.Get(id);
        }

        public List<Common.DTO.Role> GetRoles(FilterCriteria criteria)
        {
            return RoleRepository
                .Get(criteria)
                .Select(_mapper.Map)
                .ToList();
        }

        public Data_Access_Layer.User GetUser(string username)
        {
            return UserRepository
                .GetByUsername(username);
        }

        public async Task<Common.DTO.User> GetUser(int id)
        {
            var result = await UserRepository.Get(id);

            return _mapper.Map(result);
        }

        public class ClaimEqualityComparer : IEqualityComparer<System.Security.Claims.Claim>
        {
            public bool Equals(System.Security.Claims.Claim x, System.Security.Claims.Claim y)
            {
                return x.Type == y.Type &&
                    x.Value == y.Value;
            }

            public int GetHashCode(System.Security.Claims.Claim obj) => (obj.Type + obj.Value).GetHashCode();
        }

        public async Task<IList<System.Security.Claims.Claim>> GetUserClaims(Data_Access_Layer.User user)
        {
            var userClaims = UserClaimRepository
                .GetForUser(user)
                ?.Select(uc => new System.Security.Claims.Claim(uc.ClaimType, uc.ClaimValue));

            var roleClaims = RoleClaimRepository
                .GetForRole(user.UserRoles.FirstOrDefault().RoleId)
                ?.Select(uc => new System.Security.Claims.Claim(uc.ClaimType, uc.ClaimValue));

            return roleClaims
                .Union(userClaims, new ClaimEqualityComparer())
                .ToList();
        }

        public List<Common.DTO.User> GetUsers(FilterCriteria criteria)
        {
            return UserRepository
                .Get(criteria)
                .Where(usr => usr.UserStatus != Data_Access_Layer.UserStatus.DELETED)
                .OrderBy(pr => pr.UserName)
                .Select(_mapper.Map)
                .ToList();
        }

        public async Task<bool> LocationExists(string name)
        {
            return await LocationRepository.Exists(name);
        }

        public bool ProductExists(string name)
        {
            return ProductRepository
                .Entities
                .Any(pr => pr.Name == name);
        }

        public bool RoleExists(RoleExists model)
        {
            return RoleRepository
                .Entities
                .Any(co => co.Name == model.Name);
        }

        public async Task UpdateCounterparty(EditCounterparty model)
        {
            var city = GetOrCreateCity(model.City);

            var entity = new Data_Access_Layer.Counterparty
            {
                Id = model.Id,
                Name = model.Name,
                Street = model.Street,
                NIP = model.NIP,
                CityId = city.Id,
                PostalCode = model.PostalCode,
                PhoneNumber = model.PhoneNumber
            };

            CounterpartyRepository.Update(entity);
        }

        public async Task UpdateLastLogin(Data_Access_Layer.User user)
        {
            user.LastLogin = DateTime.UtcNow;
            user.SecurityStamp = Guid.NewGuid().ToString();

            UserRepository.Update(user);
        }

        public bool UserExists(UserExists model)
        {
            return UserRepository
                .Entities
                .Any(co => co.Email == model.Email || co.UserName == model.Email);
        }
    }
}
