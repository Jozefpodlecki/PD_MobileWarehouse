using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiServer.Controllers.Product.ViewModel
{
    public class AddProduct
    {
        public string Name { get; set; }

        public ICollection<ProductAttribute> ProductAttributes { get; set; }
    }
}
