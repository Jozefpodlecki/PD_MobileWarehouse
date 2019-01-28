using Common.Repository.Interfaces;
using Data_Access_Layer;
using System.Linq;

namespace Client.Repository
{
    public class ProductAttributeRepository : Repository<ProductAttribute>, IProductAttributeRepository
    {
        public ProductAttributeRepository(ISQLiteConnection sqliteConnection) : base(sqliteConnection)
        {
        }

        public bool AreUsedByProducts(int attributeId)
        {
            return Entities
                .Any(en => en.AttributeId == attributeId);
        }
    }
}