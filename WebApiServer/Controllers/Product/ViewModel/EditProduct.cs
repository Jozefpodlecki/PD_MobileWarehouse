using System.Collections.Generic;

namespace WebApiServer.Controllers.Product.ViewModel
{
    public class EditProduct
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public List<ProductAttribute> ProductAttributes { get; set; }
    }
}
