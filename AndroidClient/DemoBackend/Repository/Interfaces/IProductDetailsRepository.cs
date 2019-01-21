using System.Collections.Generic;
using Data_Access_Layer;

namespace Common.Repository.Interfaces
{
    public interface IProductDetailsRepository : IRepository<ProductDetail>
    {
        List<ProductDetail> GetForProduct(int productId);
    }
}