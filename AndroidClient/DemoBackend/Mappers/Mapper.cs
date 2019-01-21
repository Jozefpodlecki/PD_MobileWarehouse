using Common;
using Common.DTO;
using Data_Access_Layer;
using System.Linq;

namespace Common.Mappers
{
    public class Mapper :
        IMapper<Data_Access_Layer.User, Common.DTO.User>,
        IMapper<Data_Access_Layer.Role, Common.DTO.Role>,
        IMapper<Data_Access_Layer.City, Common.DTO.City>,
        IMapper<Data_Access_Layer.Location, Common.DTO.Location>,
        IMapper<Data_Access_Layer.Attribute, Common.DTO.Attribute>,
        IMapper<Data_Access_Layer.Counterparty, Common.DTO.Counterparty>,
        IMapper<Data_Access_Layer.Invoice, Common.DTO.Invoice>,
        IMapper<Data_Access_Layer.Product, Common.DTO.Product>,
        IMapper<Data_Access_Layer.Entry, Common.DTO.Entry>,
        IMapper<Data_Access_Layer.UserRole, Common.DTO.Role>,
        IMapper<Data_Access_Layer.GoodsReceivedNote, Common.DTO.GoodsReceivedNote>,
        IMapper<Data_Access_Layer.GoodsDispatchedNote, Common.DTO.GoodsDispatchedNote>,
        IMapper<Data_Access_Layer.UserClaim, Common.DTO.Claim>,
        IMapper<Data_Access_Layer.RoleClaim, Common.DTO.Claim>,
        IMapper<string, Common.DTO.Claim>
    {
        public virtual Common.DTO.City Map(Data_Access_Layer.City entity)
        {
            return new Common.DTO.City
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt,
                LastModifiedAt = entity.LastModifiedAt
        };
        }

        public virtual Common.DTO.Attribute Map(Data_Access_Layer.Attribute entity)
        {
            return new Common.DTO.Attribute
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt,
                LastModifiedAt = entity.LastModifiedAt
            };
        }

        public virtual Common.DTO.Counterparty Map(Data_Access_Layer.Counterparty entity)
        {
            return new Common.DTO.Counterparty
            {
                Id = entity.Id,
                Name = entity.Name,
                NIP = entity.NIP,
                PhoneNumber = entity.PhoneNumber,
                PostalCode = entity.PostalCode,
                Street = entity.Street,
                City = Map(entity.City),
                CreatedAt = entity.CreatedAt,
                LastModifiedAt = entity.LastModifiedAt
            };
        }

        public virtual Common.DTO.Invoice Map(Data_Access_Layer.Invoice entity)
        {
            return new Common.DTO.Invoice
            {
                Id = entity.Id,
                InvoiceType = entity.InvoiceType,
                CompletionDate = entity.CompletionDate,
                Counterparty = Map(entity.Counterparty),
                City = Map(entity.City),
                IssueDate = entity.IssueDate,
                PaymentMethod = entity.PaymentMethod,
                DocumentId = entity.DocumentId,
                Total = entity.Total,
                VAT = entity.VAT,
                Products = entity.Products
                        .Select(Map)
                        .ToList(),
                CreatedAt = entity.CreatedAt,
                LastModifiedAt = entity.LastModifiedAt
            };
        }

        public virtual Common.DTO.Product Map(Data_Access_Layer.Product entity)
        {
            return new Common.DTO.Product
            {
                Id = entity.Id,
                Name = entity.Name,
                Image = entity.Image,
                ProductAttributes = entity.ProductAttributes
                        .Select(pa => new Common.DTO.ProductAttribute
                        {
                            Attribute = Map(pa.Attribute),
                            Value = pa.Value
                        }).ToList(),
                ProductDetails = entity.ProductDetails
                        .Select(pd => new Common.DTO.ProductDetail
                        {
                            Location = Map(pd.Location),
                            Count = pd.Count
                        })
                        .ToList(),
                CreatedAt = entity.CreatedAt,
                LastModifiedAt = entity.LastModifiedAt
            };
        }

        public virtual Common.DTO.User Map(Data_Access_Layer.User entity)
        {
            return new Common.DTO.User
            {
                Id = entity.Id,
                Email = entity.Email,
                Username = entity.UserName,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Avatar = entity.Image,
                Claims = entity
                    ?.UserClaims
                    .Select(uc => new Common.DTO.Claim
                    {
                        Type = uc.ClaimType,
                        Value = uc.ClaimValue
                    })
                    .ToList(),
                Role = entity
                    ?.UserRoles
                    .Select(Map)
                    ?.FirstOrDefault(),
                CreatedAt = entity.CreatedAt,
                LastModifiedAt = entity.LastModifiedAt
            };
        }

        public virtual Common.DTO.Role Map(Data_Access_Layer.Role entity)
        {
            return new Common.DTO.Role
            {
                Id = entity.Id,
                Name = entity.Name,
                Claims = entity
                    .RoleClaims
                    .Select(Map)
                    .ToList(),
                CreatedAt = entity.CreatedAt,
                LastModifiedAt = entity.LastModifiedAt
            };
        }

        public virtual Common.DTO.Location Map(Data_Access_Layer.Location entity)
        {
            return new Common.DTO.Location
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt,
                LastModifiedAt = entity.LastModifiedAt
            };
        }

        public virtual Common.DTO.Entry Map(Data_Access_Layer.Entry entity)
        {
            return new Common.DTO.Entry
            {
                Id = entity.Id,
                Name = entity.Name,
                Count = entity.Count,
                VAT = entity.VAT,
                Price = entity.Price
            };
        }

        public Claim Map(string entity)
        {
            return new Common.DTO.Claim
            {
                Type = SiteClaimTypes.Permission,
                Value = entity
            };
        }

        public virtual Common.DTO.Role Map(UserRole entity)
        {
            return entity.Role == null ? null : new Common.DTO.Role
            {
                Id = entity.Role.Id,
                Name = entity.Role.Name,
                Claims = entity.Role
                    .RoleClaims
                    .Select(Map).ToList(),
                CreatedAt = entity.Role.CreatedAt,
                LastModifiedAt = entity.Role.LastModifiedAt
            };
        }

        public virtual Common.DTO.GoodsReceivedNote Map(Data_Access_Layer.GoodsReceivedNote entity)
        {
            return new Common.DTO.GoodsReceivedNote
            {
                DocumentId = entity.DocumentId,
                Invoice = Map(entity.Invoice)
            };
        }

        public virtual Common.DTO.GoodsDispatchedNote Map(Data_Access_Layer.GoodsDispatchedNote entity)
        {
            return new Common.DTO.GoodsDispatchedNote
            {
                DocumentId = entity.DocumentId,
                Invoice = Map(entity.Invoice)
            };
        }

        public Claim Map(RoleClaim entity)
        {
            return new Common.DTO.Claim
            {
                Type = entity.ClaimType,
                Value = entity.ClaimValue
            };
        }

        public Claim Map(UserClaim entity)
        {
            return new Common.DTO.Claim
            {
                Type = entity.ClaimType,
                Value = entity.ClaimValue
            };
        }
    }
}
