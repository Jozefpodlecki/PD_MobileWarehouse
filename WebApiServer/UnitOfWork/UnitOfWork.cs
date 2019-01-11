using Common;
using Common.DTO;
using Common.Services;
using Data_Access_Layer;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiServer.Controllers.Attribute.ViewModel;
using WebApiServer.Controllers.Counterparty.ViewModel;
using WebApiServer.Controllers.Invoice.ViewModel;
using WebApiServer.Controllers.Location.ViewModel;
using WebApiServer.Controllers.Product.ViewModel;
using WebApiServer.Controllers.Role.ViewModel;
using WebApiServer.Controllers.User.ViewModel;
using WebApiServer.Managers;
using WebApiServer.Mappers;

namespace WebApiServer
{
    public class UnitOfWork
    {
        private bool _disposed = false;
        private readonly DbContext _dbContext;
        private ProductRepository _productRepository => new ProductRepository(_dbContext);
        private AttributeRepository _attributeRepository => new AttributeRepository(_dbContext);
        private IRepository<Data_Access_Layer.ProductAttribute> _productAttributeRepository => new Repository<Data_Access_Layer.ProductAttribute>(_dbContext);
        private IRepository<Data_Access_Layer.User> _userRepository => new Repository<Data_Access_Layer.User>(_dbContext);
        private INameRepository<Data_Access_Layer.Role> _roleRepository => new NameRepository<Data_Access_Layer.Role>(_dbContext);
        private IRepository<UserRole> _userRoleRepository => new Repository<Data_Access_Layer.UserRole>(_dbContext);
        private IRepository<RoleClaim> _roleClaimRepository => new Repository<Data_Access_Layer.RoleClaim>(_dbContext);
        private IRepository<UserClaim> _userClaimRepository => new Repository<Data_Access_Layer.UserClaim>(_dbContext);
        private UserManager<Data_Access_Layer.User> _userManager;
        private RoleManager<Data_Access_Layer.Role> _roleManager;
        private INameRepository<Data_Access_Layer.Counterparty> _counterpartyRepository => new NameRepository<Data_Access_Layer.Counterparty>(_dbContext);
        private INameRepository<Data_Access_Layer.City> _cityRepository => new NameRepository<Data_Access_Layer.City>(_dbContext);
        private InvoiceRepository _invoiceRepository => new InvoiceRepository(_dbContext);
        private EntryRepository _entryRepository => new EntryRepository(_dbContext);
        private IRepository<Data_Access_Layer.GoodsDispatchedNote> _goodsDispatchedNoteRepository => new Repository<Data_Access_Layer.GoodsDispatchedNote>(_dbContext);
        private IRepository<Data_Access_Layer.GoodsReceivedNote> _goodsReceivedNoteRepository => new Repository<Data_Access_Layer.GoodsReceivedNote>(_dbContext);
        private ProductDetailsRepository _productDetailsRepository => new ProductDetailsRepository(_dbContext);
        private LocationRepository _locationRepository => new LocationRepository(_dbContext);
        private Mapper _mapper;

        private readonly PasswordManager _passwordManager;

        public UnitOfWork(
            IUserResolverService userResolverService,
            PasswordManager passwordManager,
            UserManager<Data_Access_Layer.User> userManager,
            RoleManager<Data_Access_Layer.Role> roleManager,
            DbContext dbContext)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _passwordManager = passwordManager;

            if (userResolverService.CanUserSeeDetails())
            {
                _mapper = new DetailsMapper();
            }
            else
            {
                _mapper = new Mapper();
            }
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
            var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            var userClaims = await _userManager.GetClaimsAsync(user);

            var role = await _roleManager.FindByNameAsync(userRole);
            var roleClaims = await _roleManager.GetClaimsAsync(role);
            
            return roleClaims.Union(userClaims).ToList();
        }

        public Data_Access_Layer.User GetUser(string username)
        {
            return _userManager
                .Users
                .FirstOrDefault(us => us.UserName == username);
        }

        public async Task UpdateLastLogin(Data_Access_Layer.User user)
        {
            user.LastLogin = DateTime.UtcNow;
            user.SecurityStamp = Guid.NewGuid().ToString();

            await _userManager.UpdateAsync(user);

            Save();
        }

        public async Task AddUser(AddUser model)
        {
            var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                var userRepository = _userRepository;
                var roleRepository = _roleRepository;
                var userRoleRepository = _userRoleRepository;
                var userClaimRepository = _userClaimRepository;

                var passwordHash = _passwordManager.GetHash(model.Password);

                var entity = new Data_Access_Layer.User()
                {
                    UserName = model.Username,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PasswordHash = passwordHash
                };

                await userRepository.Add(entity);
                Save();

                if (model.Role != null)
                {
                    var role = await roleRepository.Find(model.Role.Name);

                    if (role != null)
                    {

                        await userRoleRepository.Add(
                            new UserRole
                            {
                                User = entity,
                                Role = role
                            });

                        Save();
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

                    await userClaimRepository.AddRange(claims);
                }

                Save();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task AddUsers(IEnumerable<AddUser> users)
        {
            var transaction = _dbContext.Database.BeginTransaction();
            var userRepository = _userRepository;
            var roleRepository = _roleRepository;
            var userRoleRepository = _userRoleRepository;
            var userClaimRepository = _userClaimRepository;

            try
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

                    await userRepository.Add(entity);
                    Save();

                    if (model.Role != null)
                    {
                        var role = await roleRepository.Find(model.Role.Name);

                        if (role != null)
                        {

                            await userRoleRepository.Add(
                                new UserRole
                                {
                                    UserId = entity.Id,
                                    RoleId = role.Id
                                });

                            Save();
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

                        await userClaimRepository.AddRange(claims);
                    }

                    Save();
                }
                
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task EditUser(EditUser model)
        {
            var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                var userRepository = _userRepository;
                var roleRepository = _roleRepository;
                var userClaimRepository = _userClaimRepository;
                var userRoleRepository = _userRoleRepository;
                var entity = await userRepository.Get(model.Id);

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
                Save();

                if (model.Role != null)
                {
                    var role = await roleRepository.Find(model.Role.Name);

                    await userRoleRepository.Add(
                    new UserRole
                    {
                        User = entity,
                        Role = role
                    });
                }

                userRepository.Update(entity);
                Save();

                entity.UserClaims.Clear();

                if (model.Claims != null)
                {
                    var claims = model.Claims.Select(cl => new UserClaim
                    {
                        UserId = entity.Id,
                        ClaimType = cl.Type,
                        ClaimValue = cl.Value
                    });

                    await userClaimRepository.AddRange(claims);
                }

                Save();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex; 
            }
        }

        public async Task EditProduct(EditProduct model)
        {
            var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                var productRepository = _productRepository;
                var attributeRepository = _attributeRepository;
                var productAttributeRepository = _productAttributeRepository;

                var product = await productRepository.Get(model.Id);

                product.Image = model.Image;

                productRepository.Update(product);

                Save();

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

                await attributeRepository.AddRange(attributesToAdd);
                Save();

                var productAttributesToAdd = attributeRepository
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

                await productAttributeRepository.AddRange(productAttributesToAdd);
                Save();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }            
        }

        public void BlockUser(Data_Access_Layer.User user)
        {
            var userRepository = _userRepository;

            user.UserStatus = UserStatus.BLOCKED;

            userRepository.Update(user);

            Save();
        }

        public async Task AddRoles(IEnumerable<AddRole> roles)
        {
            var transaction = _dbContext.Database.BeginTransaction();
            var roleRepository = _roleRepository;
            var roleClaimRepository = _roleClaimRepository;

            try
            {
                foreach (var model in roles)
                {
                    var entity = new Data_Access_Layer.Role()
                    {
                        Name = model.Name,
                        NormalizedName = model.Name.ToUpper()
                    };

                    await roleRepository.Add(entity);
                    Save();

                    await roleClaimRepository
                        .AddRange(model.Claims.Select(cl => new RoleClaim
                        {
                            Role = entity,
                            ClaimType = cl.Type,
                            ClaimValue = cl.Value
                        }));

                    Save();
                }
                
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        public async Task AddRole(AddRole model)
        {
            var transaction = _dbContext.Database.BeginTransaction();
            var roleRepository = _roleRepository;
            var roleClaimRepository = _roleClaimRepository;

            try
            {
                var entity = new Data_Access_Layer.Role()
                {
                    Name = model.Name,
                    NormalizedName = model.Name.ToUpper()
                };

                await roleRepository.Add(entity);
                Save();

                await roleClaimRepository
                    .AddRange(model.Claims.Select(cl => new RoleClaim
                    {
                        Role = entity,
                        ClaimType = cl.Type,
                        ClaimValue = cl.Value
                    }));

                Save();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        public async Task EditRole(EditRole model)
        {
            var transaction = _dbContext.Database.BeginTransaction();
            var roleRepository = _roleRepository;
            var roleClaimRepository = _roleClaimRepository;
            
            try
            {
                var entity = await roleRepository.Get(model.Id);

                entity.Name = model.Name;
                entity.NormalizedName = model.Name.ToUpper();

                entity.RoleClaims.Clear();

                await roleClaimRepository
                    .AddRange(model.Claims.Select(cl => new RoleClaim
                    {
                        Role = entity,
                        ClaimType = cl.Type,
                        ClaimValue = cl.Value
                    }));

                roleRepository.Update(entity);
                Save();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        public async Task DeleteUser(int id)
        {
            var userRepository = _userRepository;

            var user = await userRepository.Get(id);

            user.UserStatus = UserStatus.DELETED;

            userRepository.Update(user);

            Save();
        }

        public async Task DeleteAttribute(int id)
        {
            var attributeRepository = _attributeRepository;
            var entity = await attributeRepository.Get(id);

            attributeRepository.Remove(entity);
        }

        public async Task DeleteLocation(int id)
        {
            var locationRepository = _locationRepository;

            var location = await locationRepository.Get(id);
            locationRepository.Remove(location);

            Save();
        }

        public void DeleteRole(Data_Access_Layer.Role role)
        {
            var roleRepository = _roleRepository;

            roleRepository.Remove(role);
            Save();
        }

        public async Task<bool> LocationExists(string name)
        {
            return await _locationRepository.Exists(name);
        }

        public bool ProductExists(string name)
        {
            return _productRepository
                .Entities
                .Any(pr => pr.Name == name);
        }

        public bool RoleExists(RoleExists model)
        {
            return _roleRepository
                .Entities
                .Any(co => co.Name == model.Name);
        }

        public bool UserExists(UserExists model)
        {
            var userRepository = _userRepository;

            return userRepository
                .Entities
                .Any(co => co.Email == model.Email || co.UserName == model.Email);
        }

        public bool ExistsCounterparty(ExistsCounterparty model)
        {
            return _counterpartyRepository
                .Entities
                .Any(co => co.NIP == model.NIP || co.Name == model.Name);
        }

        public async Task AddCounterparty(AddCounterparty model)
        {
            var counterpartyRepository = _counterpartyRepository;

            var city = await GetOrAddCity(model.City);

            var entity = new Data_Access_Layer.Counterparty
            {
                Name = model.Name,
                Street = model.Street,
                NIP = model.NIP,
                CityId = city.Id,
                PostalCode = model.PostalCode,
                PhoneNumber = model.PhoneNumber
            };

            await counterpartyRepository.Add(entity);
            Save();
        }

        public async Task UpdateCounterparty(EditCounterparty model)
        {
            var counterpartyRepository = _counterpartyRepository;

            var city = await GetOrAddCity(model.City);

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

            counterpartyRepository.Update(entity);

            Save();
        }

        private async Task<Data_Access_Layer.City> GetOrAddCity(Common.DTO.City model)
        {
            var cityRepository = _cityRepository;

            Data_Access_Layer.City city = null;

            if (model.Id == 0)
            {

                city = await cityRepository.Find(model.Name);

                if (city == null)
                {
                    city = new Data_Access_Layer.City()
                    {
                        Name = model.Name
                    };

                    await cityRepository.Add(city);
                    await cityRepository.Save();
                }
            }
            else
            {
                city = await cityRepository.Get(model.Id);
            }

            return city;
        }

        public List<KeyValue> GetPaymentMethods()
        {
            return InvoiceRepository.PaymentMethods;
        }

        public List<KeyValue> GetInvoiceTypes()
        {
            return InvoiceRepository.InvoiceTypes;
        }

        public async Task AddAttribute(AddAttribute model)
        {
            var attributeRepository = _attributeRepository;

            var entity = new Data_Access_Layer.Attribute
            {
                Name = model.Name
            };

            await attributeRepository.Add(entity);
            Save();
        }

        public async Task EditAttribute(EditAttribute model)
        {
            var attributeRepository = _attributeRepository;

            var entity = new Data_Access_Layer.Attribute
            {
                Id = model.Id,
                Name = model.Name
            };

            attributeRepository.Update(entity);
            Save();
        }

        public async Task AddLocation(AddLocation model)
        {
            var locationRepository = _locationRepository;

            var location = new Data_Access_Layer.Location()
            {
                Name = model.Name
            };

            await locationRepository.Add(location);
            Save();
        }

        public async Task EditLocation(EditLocation model)
        {
            var locationRepository = _locationRepository;

            var location = new Data_Access_Layer.Location()
            {
                Id = model.Id,
                Name = model.Name
            };

           locationRepository.Update(location);

           Save();
        }

        public async Task AddGoodsDispatchedNote(Controllers.Note.ViewModel.AddGoodsDispatchedNote model)
        {
            var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                var goodsDispatchedNoteRepository = _goodsDispatchedNoteRepository;
                var productRepository = _productRepository;
                var entryRepository = _entryRepository;
                var productDetailsRepository = _productDetailsRepository;

                var note = new Data_Access_Layer.GoodsDispatchedNote
                {
                    DocumentId = model.DocumentId,
                    IssueDate = model.IssueDate,
                    DispatchDate = model.DispatchDate,
                    InvoiceId = model.InvoiceId
                };

                await goodsDispatchedNoteRepository.Add(note);
                Save();

                var invoiceEntries = await entryRepository.GetForInvoice(model.InvoiceId);

                foreach (var noteEntry in model.NoteEntry)
                {
                    var productEntity = await productRepository.Find(noteEntry.Name);

                    var entry = invoiceEntries
                        .FirstOrDefault(ie => ie.Name == noteEntry.Name);

                    var productDetails = productDetailsRepository
                        .GetForProduct(productEntity.Id);

                    var productDetail = productDetails
                        .FirstOrDefault(pd => pd.Location.Id == noteEntry.Location.Id);

                    productDetail.Count -= entry.Count;

                    if(productDetail.Count <= 0)
                    {
                        productDetailsRepository.Remove(productDetail);

                        Save();
                    }
                    else
                    {
                        productDetailsRepository.Update(productDetail);

                        Save();
                    }

                    if(productDetails.Count == 1)
                    {
                        _productRepository.Remove(productEntity);
                        Save();
                    }
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        public async Task AddGoodsReceivedNote(Controllers.Note.ViewModel.AddGoodsReceivedNote model)
        {
            var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                var productRepository = _productRepository;
                var goodsReceivedNoteRepository = _goodsReceivedNoteRepository;
                var entryRepository = _entryRepository;
                var productDetailsRepository = _productDetailsRepository;

                var note = new Data_Access_Layer.GoodsReceivedNote
                {
                    DocumentId = model.DocumentId,
                    IssueDate = model.IssueDate,
                    ReceiveDate = model.ReceiveDate,
                    InvoiceId = model.InvoiceId
                };

                await goodsReceivedNoteRepository.Add(note);
                Save();

                var invoiceEntries = await entryRepository.GetForInvoice(model.InvoiceId);

                foreach (var noteEntry in model.NoteEntry)
                {
                    var productEntity = await productRepository.Find(noteEntry.Name);

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

                        await productRepository.Add(product);
                        Save();

                        productDetail = new Data_Access_Layer.ProductDetail
                        {
                            LocationId = noteEntry.Location.Id,
                            ProductId = product.Id,
                            Count = entry.Count,
                        };

                        await productDetailsRepository.Add(productDetail);
                        Save();

                        continue;
                    }

                    var productDetails = productDetailsRepository
                        .GetForProduct(productEntity.Id)
                        .FirstOrDefault(pd => pd.LocationId == noteEntry.Location.Id);

                    productDetails.Count += entry.Count;

                    productDetailsRepository.Update(productDetails);

                    Save();

                }
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
        }

        public async Task CreateInvoice(AddInvoice model)
        {
            var transaction = _dbContext.Database.BeginTransaction();

            try
            {
                var entryRepository = _entryRepository;
                var invoiceRepository = _invoiceRepository;

                Data_Access_Layer.Invoice invoice = null;
                Data_Access_Layer.City city = null;

                if (model.City != null)
                {
                    city = await GetOrAddCity(model.City);
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

                await invoiceRepository.Add(invoice);
                Save();

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

                await entryRepository.AddRange(entries);
                Save();

                invoice.Total = total;
                invoice.VAT = totalVAT;

                invoiceRepository.Update(invoice);
                Save();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task CreateInvoices(IEnumerable<AddInvoice> models)
        {
            var transaction = _dbContext.Database.BeginTransaction();
            var entryRepository = _entryRepository;
            var invoiceRepository = _invoiceRepository;

            try
            {
                foreach (var model in models)
                {
                    Data_Access_Layer.Invoice invoice = null;
                    Data_Access_Layer.City city = null;

                    if (model.City != null)
                    {
                        city = await GetOrAddCity(model.City);
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

                    await invoiceRepository.Add(invoice);
                    Save();

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

                    await entryRepository.AddRange(entries);
                    Save();

                    invoice.Total = total;
                    invoice.VAT = totalVAT;

                    invoiceRepository.Update(invoice);
                    Save();
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<List<Common.DTO.Attribute>> GetAttributes(FilterCriteria criteria)
        {
            var attributeRepository = _attributeRepository;

            var result = attributeRepository.Entities;

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                result = result.Where(en => en.Name.Contains(criteria.Name));
            }

            return await result
                .Skip(criteria.Page * criteria.ItemsPerPage)
                .Take(criteria.ItemsPerPage)
                .Select(at => new Common.DTO.Attribute
                {
                   Id = at.Id,
                   Name = at.Name
                })
                .ToListAsync();
        }

        public List<Common.DTO.GoodsReceivedNote> GetGoodsReceivedNotes(FilterCriteria criteria)
        {
            var goodsReceivedNoteRepository = _goodsReceivedNoteRepository;

            var result = goodsReceivedNoteRepository
                .Entities;

            return Helpers.Paging.GetPaged(result, criteria)
                .Select(grn => new Common.DTO.GoodsReceivedNote
                {
                    DocumentId = grn.DocumentId,
                    Invoice = new Common.DTO.Invoice
                    {

                    }
                })
                .ToList();
        }

        public List<Common.DTO.GoodsDispatchedNote> GetGoodsDispatchedNotes(FilterCriteria criteria)
        {
            var goodsDispatchedNoteRepository = _goodsDispatchedNoteRepository;

            var result = goodsDispatchedNoteRepository
                .Entities;

            return Helpers.Paging.GetPaged(result, criteria)
                .Select(_mapper.Map)
                .ToList();
        }

        public List<Common.DTO.Invoice> GetInvoices(InvoiceFilterCriteria criteria)
        {
            var invoiceRepository = _invoiceRepository;

            var result = invoiceRepository.Entities;

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

        public List<Common.DTO.User> GetUsers(FilterCriteria criteria)
        {
            var userRepository = _userRepository;

            var query = userRepository
                .Entities
                .Skip(criteria.Page * criteria.ItemsPerPage)
                .Take(criteria.ItemsPerPage);

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                query = query.Where(pt => pt.UserName.Contains(criteria.Name));
            }

            return query
                .Where(usr => usr.UserStatus != Data_Access_Layer.UserStatus.DELETED)
                .OrderBy(pr => pr.UserName)
                .Select(_mapper.Map)
                .ToList();
        }

        public List<Common.DTO.Claim> GetClaims()
        {
            return PolicyTypes
                .Properties
                .Select(_mapper.Map)
                .ToList();
        }

        public List<Common.DTO.Location> GetLocations(FilterCriteria criteria)
        {
            var result = _locationRepository
               .Entities;

            return Helpers.Paging.GetPaged(result, criteria)
                .Select(_mapper.Map)
                .ToList();
        }

        public List<Common.DTO.Role> GetRoles(FilterCriteria criteria)
        {
            var roleRepository = _roleRepository;

            var query = roleRepository
                .Entities;

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                query = query.Where(pt => pt.Name.Contains(criteria.Name));
            }

            query = query
                .Skip(criteria.Page * criteria.ItemsPerPage)
                .Take(criteria.ItemsPerPage);

            return query
                .Select(_mapper.Map)
                .ToList();
        }

        public List<Common.DTO.Product> GetProducts(FilterCriteria criteria)
        {
            var productRepository = _productRepository;

            var query = productRepository
                .Entities
                .Skip(criteria.Page * criteria.ItemsPerPage)
                .Take(criteria.ItemsPerPage);

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                query = query.Where(pt => pt.Name.Contains(criteria.Name));
            }

            return query.OrderBy(pr => pr.Name)
                .Select(_mapper.Map)
                .ToList();
        }

        public List<Common.DTO.Counterparty> GetCounterparties(FilterCriteria criteria)
        {
            var counterpartyRepository = _counterpartyRepository;

            var result = counterpartyRepository
                .Entities;

            return Helpers.Paging.GetPaged(result, criteria)
                .Select(_mapper.Map)
                .ToList();
        }

        public List<Common.DTO.City> GetCities(FilterCriteria criteria)
        {
            var cityRepository = _cityRepository;

            var result = cityRepository
                .Entities;

            return Helpers.Paging.GetPaged(result, criteria)
                .Select(_mapper.Map)
                .ToList();
        }

        public async Task<Common.DTO.User> GetUser(int id)
        {
            var result = await _userRepository.Get(id);

            return _mapper.Map(result);
        }

        public async Task<Data_Access_Layer.Role> GetRole(int id)
        {
            return await _roleRepository.Get(id);
        }

        public Common.DTO.Product GetProductByBarcode(string barcode)
        {
            return _productRepository
                .Entities
                .Select(_mapper.Map)
                .FirstOrDefault(pr => pr.Barcode == barcode);
        }

        public List<Common.DTO.Location> GetLocationsByProduct(string name)
        {
            return _locationRepository
                .GetLocationsByProduct(name)
                .Select(_mapper.Map)
                .ToList();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
