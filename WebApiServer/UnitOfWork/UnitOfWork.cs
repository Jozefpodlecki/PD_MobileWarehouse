using Common;
using Common.DTO;
using Data_Access_Layer;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiServer.Controllers.Product.ViewModel;
using WebApiServer.Managers;

namespace WebApiServer
{
    public class UnitOfWork
    {
        private bool _disposed = false;
        private readonly DbContext _dbContext;
        public readonly ProductRepository ProductRepository;
        public readonly AttributeRepository AttributeRepository;
        public readonly IRepository<Data_Access_Layer.ProductAttribute> ProductAttributeRepository;
        public readonly IRepository<Data_Access_Layer.User> UserRepository;
        public readonly INameRepository<Data_Access_Layer.Role> RoleRepository;
        public readonly IRepository<UserRole> UserRoleRepository;
        public readonly IRepository<RoleClaim> RoleClaimRepository;
        public readonly IRepository<UserClaim> UserClaimRepository;
        public readonly UserManager<Data_Access_Layer.User> UserManager;
        public readonly RoleManager<Data_Access_Layer.Role> RoleManager;
        public readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PasswordManager _passwordManager;

        public UnitOfWork(
            IHttpContextAccessor httpContextAccessor,
            UserManager<Data_Access_Layer.User> userManager,
            RoleManager<Data_Access_Layer.Role> roleManager,
            PasswordManager passwordManager,
            DbContext dbContext)
        {
            _dbContext = dbContext;
            ProductRepository = new ProductRepository(dbContext);
            AttributeRepository = new AttributeRepository(dbContext);
            ProductAttributeRepository = new Repository<Data_Access_Layer.ProductAttribute>(dbContext);
            UserRepository = new Repository<Data_Access_Layer.User>(dbContext);
            RoleRepository = new NameRepository<Data_Access_Layer.Role>(dbContext);
            UserRoleRepository = new Repository<UserRole>(dbContext); ;
            RoleClaimRepository = new Repository<RoleClaim>(dbContext); ;
            UserClaimRepository = new Repository<UserClaim>(dbContext);
            ProductAttributeRepository = new Repository<Data_Access_Layer.ProductAttribute>(dbContext);
            _httpContextAccessor = httpContextAccessor;
            _passwordManager = passwordManager;
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public async Task AddUser(Common.DTO.User user)
        {
            var passwordHash = _passwordManager.GetHash(user.Password);

            var entity = new Data_Access_Layer.User()
            {
                UserName = user.Username,
                Email = user.Email,
                PasswordHash = passwordHash
            };

            await UserRepository.Add(entity);
            Save();

            if(user.Role != null)
            {
                var role = await RoleRepository.Find(user.Role);

                await UserRoleRepository.Add(new UserRole
                {
                    User = entity,
                    Role = role
                });

                Save();
            }
            
            var claims = user.Claims.Select(cl => new UserClaim
            {
                UserId = entity.Id,
                ClaimType = cl.Type,
                ClaimValue = cl.Value
            });

            await UserClaimRepository.AddRange(claims);
            Save();
        }

        public async Task EditUser(Common.DTO.User user)
        {
            var passwordHash = _passwordManager.GetHash(user.Password);

            var entity = await UserRepository.Get(user.Id);

            entity.UserName = user.Username;
            entity.Email = user.Email;

            if (!_passwordManager.Compare(entity, passwordHash)) {
                entity.PasswordHash = passwordHash;
            }

            entity.UserClaims.Clear();
            entity.UserRoles.Clear();

            var role = await RoleRepository.Find(user.Role);

            await UserRoleRepository.Add(new UserRole
            {
                User = entity,
                Role = role
            });

            UserRepository.Update(entity);
            Save();

            var claims = user.Claims.Select(cl => new UserClaim
            {
                UserId = entity.Id,
                ClaimType = cl.Type,
                ClaimValue = cl.Value
            });

            await UserClaimRepository.AddRange(claims);

            Save();
        }

        public void DeleteUser(Data_Access_Layer.User user)
        {
            user.UserStatus = UserStatus.DELETED;

            UserRepository.Update(user);
        }

        public void BlockUser(Data_Access_Layer.User user)
        {
            user.UserStatus = UserStatus.BLOCKED;

            UserRepository.Update(user);
        }

        public async Task AddRole(Common.DTO.Role role)
        {
            var entity = new Data_Access_Layer.Role()
            {
                Name = role.Name,
                NormalizedName = role.Name.ToUpper()
            };

            await RoleRepository.Add(entity);
            Save();

            await RoleClaimRepository.AddRange(role.Claims.Select(cl => new RoleClaim
            {
                Role = entity,
                ClaimType = cl.Type,
                ClaimValue = cl.Value
            }));

            Save();
        }

        public async Task EditRole(Common.DTO.Role role)
        {
            var entity = await RoleRepository.Get(role.Id);

            entity.Name = role.Name;
            entity.NormalizedName = role.Name.ToUpper();

            entity.RoleClaims.Clear();

            await RoleClaimRepository.AddRange(role.Claims.Select(cl => new RoleClaim
            {
                Role = entity,
                ClaimType = cl.Type,
                ClaimValue = cl.Value
            }));

            RoleRepository.Update(entity);
            Save();
        }

        public void DeleteRole(Data_Access_Layer.Role role)
        {
            RoleRepository.Remove(role);
        }

        public List<BasicProduct> GetProducts(FilterCriteria criteria)
        {
            var query = ProductRepository
                .Entities
                .Skip(criteria.Page * criteria.ItemsPerPage)
                .Take(criteria.ItemsPerPage);

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                query = query.Where(pt => pt.Name.Contains(criteria.Name));
            }

            return query.OrderBy(pr => pr.Name)
                .Select(pr => new BasicProduct
            {
                Id = pr.Id,
                Name = pr.Name,
                Image = pr.Image,
                LastModification = pr.LastModification
            })
            .ToList();
        }

        public async Task AddProduct(AddProduct model)
        {
            var claimsPrincipal = _httpContextAccessor.HttpContext.User;
            var user = await UserManager.GetUserAsync(claimsPrincipal);

            var product = new Data_Access_Layer.Product
            {
                Name = model.Name,
                //CreatedBy = user,
                CreatedById = user.Id,
                LastModificationById = user.Id
            };

            await ProductRepository.Add(product);

            var attrs = AttributeRepository
                .Entities
                .Where(at => model.ProductAttributes.Any(pa => pa.Attribute.Name == at.Name))
                .Select(ar => ar.Name)
                .ToList();

            var attributes = model.ProductAttributes
                .Where(pa => !attrs.Contains(pa.Attribute.Name))
                .Select(pa => new Data_Access_Layer.Attribute
                {
                    Name = pa.Attribute.Name,
                    CreatedById = user.Id,
                    LastModificationById = user.Id
                });

            await AttributeRepository.AddRange(attributes);

            await _dbContext.SaveChangesAsync();

            var productAttributes = AttributeRepository
                .Entities
                .Join(model.ProductAttributes, pa => pa.Name, pa => pa.Attribute.Name, (a, b) => 
                new Data_Access_Layer.ProductAttribute
                {
                    Product = product,
                    Attribute = a,
                    Value = b.Value,
                })
                .ToList();

            await ProductAttributeRepository.AddRange(productAttributes);
            
        }


        public void Save()
        {
            _dbContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
