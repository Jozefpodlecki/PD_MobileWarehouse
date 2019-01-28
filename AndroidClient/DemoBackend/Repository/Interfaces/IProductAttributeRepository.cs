using Common.Repository.Interfaces;
using Data_Access_Layer;

namespace Common.Repository.Interfaces
{
    public interface IProductAttributeRepository : IRepository<ProductAttribute>
    {
        bool AreUsedByProducts(int attributeId);
    }
}